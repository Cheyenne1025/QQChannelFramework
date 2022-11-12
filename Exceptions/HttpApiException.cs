namespace QQChannelFramework.Exceptions; 

public class HttpApiException : Exception {
    
    public HttpApiException(Exception inner, string traceId) : base($"TraceId: {traceId}\nInner Exception: {inner}", inner) {
        
    }
}