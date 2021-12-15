namespace QQChannelFramework.Exceptions; 

public class ErrorResultException : Exception {
    public int Code { get; private set; }

    public ErrorResultException(int code, string message) : base(message) {
        Code = code;
    }
}