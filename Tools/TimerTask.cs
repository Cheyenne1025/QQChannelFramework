using System.Threading;
using System.Threading.Tasks;
namespace MyBot.Tools {
   public class TimerTask : IDisposable {
      private Task _loop;
      private CancellationTokenSource _source = new();

      public TimerTask(Action<CancellationToken> action, TimeSpan duration) {
         _loop = Task.Factory.StartNew(async () => {
            await Task.Delay(3000, _source.Token).ConfigureAwait(false);
            while (!_source.Token.IsCancellationRequested) {
               try {
                  using var ts = new CancellationTokenSource(TimeSpan.FromSeconds(300));
                  action(ts.Token);
               } catch (Exception ex) when (ex is AggregateException or TaskCanceledException) {
                  BotLog.Log("定时任务超时");
                  BotLog.Log(ex);
               } catch (Exception ex) {
                  BotLog.Log("定时任务异常");
                  BotLog.Log(ex);
               }

               await Task.Delay(duration, _source.Token).ConfigureAwait(false);
            }
         }, TaskCreationOptions.LongRunning);
      }

      public TimerTask(Action action, TimeSpan duration) {
         _loop = Task.Factory.StartNew(async () => {
            await Task.Delay(3000, _source.Token).ConfigureAwait(false);
            while (!_source.Token.IsCancellationRequested) {
               try {
                  using var ts = new CancellationTokenSource(TimeSpan.FromSeconds(300));
                  action();
               } catch (Exception ex) when (ex is AggregateException or TaskCanceledException) {
                  BotLog.Log("定时任务超时");
                  BotLog.Log(ex);
               } catch (Exception ex) {
                  BotLog.Log("定时任务异常");
                  BotLog.Log(ex);
               }

               await Task.Delay(duration, _source.Token).ConfigureAwait(false);
            }
         }, TaskCreationOptions.LongRunning);
      }

      public TimerTask(Func<Task> action, TimeSpan duration) {
         _loop = Task.Factory.StartNew(async () => {
            await Task.Delay(3000, _source.Token).ConfigureAwait(false);
            while (!_source.Token.IsCancellationRequested) {
               try {
                  using var ts = new CancellationTokenSource(TimeSpan.FromSeconds(300));
                  await action();
               } catch (Exception ex) when (ex is AggregateException or TaskCanceledException) {
                  BotLog.Log("定时任务超时");
                  BotLog.Log(ex);
               } catch (Exception ex) {
                  BotLog.Log("定时任务异常");
                  BotLog.Log(ex);
               }

               await Task.Delay(duration, _source.Token).ConfigureAwait(false);
            }
         }, TaskCreationOptions.LongRunning);
      }

      public TimerTask(Func<CancellationToken, Task> action, TimeSpan duration) {
         _loop = Task.Factory.StartNew(async () => {
            await Task.Delay(3000, _source.Token).ConfigureAwait(false);
            while (!_source.Token.IsCancellationRequested) {
               try {
                  using var ts = new CancellationTokenSource(TimeSpan.FromSeconds(300));
                  await action(ts.Token);
               } catch (Exception ex) when (ex is AggregateException or TaskCanceledException) {
                  BotLog.Log("定时任务超时");
                  BotLog.Log(ex);
               } catch (Exception ex) {
                  BotLog.Log("定时任务异常");
                  BotLog.Log(ex);
               }

               await Task.Delay(duration, _source.Token).ConfigureAwait(false);
            }
         }, TaskCreationOptions.LongRunning);
      }

      public TimerTask(TimeSpan firstWait, Func<Task> action, TimeSpan duration) {
         _loop = Task.Factory.StartNew(async () => {
            await Task.Delay(firstWait, _source.Token).ConfigureAwait(false);
            while (!_source.Token.IsCancellationRequested) {
               try {
                  using var ts = new CancellationTokenSource(TimeSpan.FromSeconds(300));
                  await action();
               } catch (Exception ex) when (ex is AggregateException or TaskCanceledException) {
                  BotLog.Log("定时任务超时");
                  BotLog.Log(ex);
               } catch (Exception ex) {
                  BotLog.Log("定时任务异常");
                  BotLog.Log(ex);
               }

               await Task.Delay(duration, _source.Token).ConfigureAwait(false);
            }
         }, TaskCreationOptions.LongRunning);
      }

      public void Dispose() {
         try {
            _source.Cancel();
            _loop.Wait(1000, _source.Token);
            _source.Dispose();
            _loop.Dispose();
         } catch (OperationCanceledException) { } catch (Exception ex) {
            BotLog.Log("退出定时异常");
            BotLog.Log(ex);
         }
      }
   }
}
