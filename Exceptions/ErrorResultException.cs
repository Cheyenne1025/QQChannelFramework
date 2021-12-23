namespace QQChannelFramework.Exceptions; 

public class ErrorResultException : Exception {
    public int Code { get; private set; }

    public override string Message => $"({Code}) " + base.Message;
    
    public ErrorResultException(int code, string message) : base(message) {
        Code = code; 
    }
}