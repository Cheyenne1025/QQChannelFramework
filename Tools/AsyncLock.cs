using System.Threading;
using System.Threading.Tasks;
namespace MajsoulBot.Utils;

public class AsyncLock {
   private readonly Task<IDisposable> _releaserTask;
   private readonly SemaphoreSlim _semaphore = new(1, 1);
   private readonly IDisposable _releaser;

   public bool IsLocked => _semaphore.CurrentCount == 0;

   public AsyncLock() {
      _releaser = new Releaser(_semaphore);
      _releaserTask = Task.FromResult(_releaser);
   }

   /// <summary>Use inside 'using' block</summary>
   public IDisposable Lock() {
      _semaphore.Wait();
      return _releaser;
   }

   /// <summary>Use inside 'using' block with await</summary>
   public Task<IDisposable> LockAsync() {
      Task task = _semaphore.WaitAsync();
      return !task.IsCompleted
         ? task.ContinueWith((Func<Task, object, IDisposable>) ((_, releaser) => (IDisposable) releaser), _releaser,
            CancellationToken.None, TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default)
         : _releaserTask;
   }

   private class Releaser : IDisposable {
      private readonly SemaphoreSlim _semaphore;

      public Releaser(SemaphoreSlim semaphore) => _semaphore = semaphore;

      public void Dispose() => _semaphore.Release();
   }
}