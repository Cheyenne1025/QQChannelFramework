using QQChannelFramework.Api.Base;

namespace QQChannelFramework.Api;

sealed partial class QQChannelApi {
    public ForumApi GetForumApi() {
        return new(apiBase);
    }
}

public class ForumApi
{
    readonly ApiBase _apiBase;

    public ForumApi(ApiBase apiBase) {
        _apiBase = apiBase;
    }
}