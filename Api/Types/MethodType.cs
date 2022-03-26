using System.Net.Http;

namespace QQChannelFramework.Api.Types;

public enum MethodType {
    GET,
    POST,
    DELETE,
    PATCH,
    PUT
}

public static class MethodTypeHelper {
    public static HttpMethod ToHttpMethod(this MethodType t) {
        return t switch {
            MethodType.DELETE => HttpMethod.Delete,
            MethodType.GET => HttpMethod.Get,
            MethodType.PATCH => HttpMethod.Patch,
            MethodType.POST => HttpMethod.Post,
            MethodType.PUT => HttpMethod.Put,
            _ => throw new ArgumentOutOfRangeException(nameof(t), t, null)
        };
    }
}