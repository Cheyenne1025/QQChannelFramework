namespace MyBot.Tools;

/// <summary>
/// 
/// </summary>
public static class BotLog {
    /// <summary>
    /// 
    /// </summary>
    public static event Action<string> OnLogMessage;
    
    /// <summary>
    /// 
    /// </summary>
    public static event Action<Exception> OnLogException;
 
    internal static void Log(string message) {
        OnLogMessage?.Invoke(message);
    }
    
    internal static void Log(Exception ex) {
        OnLogException?.Invoke(ex);
    }
}