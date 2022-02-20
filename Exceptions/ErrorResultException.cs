namespace QQChannelFramework.Exceptions; 

public class ErrorResultException : Exception {
    public int Code { get; private set; }
    public string TraceId { get; private set; }

    public override string Message => $"TraceId: {TraceId} ({Code}) " + base.Message;
    
    public ErrorResultException(int code, string message, string traceId) : base(message) {
        Code = code;
        TraceId = traceId;
    }
}