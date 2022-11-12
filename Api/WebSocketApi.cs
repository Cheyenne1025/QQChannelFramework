using System.Threading.Tasks;
using QQChannelFramework.Api.Base;
using QQChannelFramework.Api.Raws;
using QQChannelFramework.Models;

namespace QQChannelFramework.Api;

sealed partial class QQChannelApi
{ 
    public WebSocketApi GetWebSocketApi()
    { 
        return new(apiBase);
    }
}

public class WebSocketApi
{
    readonly ApiBase _apiBase;

    public WebSocketApi(ApiBase apiBase)
    {
        _apiBase = apiBase;
    }

    /// <summary>
    /// 获取 WSS 通用接入点
    /// </summary>
    /// <returns>通用接入点</returns>
    public async Task<string> GetUrlAsync()
    {
        RawGetWebSocketUrlApi rawGetWebSocketUrlApi;

        var requestData = await _apiBase.RequestAsync(rawGetWebSocketUrlApi).ConfigureAwait(false);

        return requestData["url"]!.ToString();
    }

    /// <summary>
    /// 获取带分片 WSS 接入点
    /// </summary>
    /// <returns>元组 (分片接入点基础信息，分片会话限制信息)</returns>
    public async Task<ShardWssInfo> GetShardUrlAsync()
    {
        RawGetWebSocketShardUrl rawGetWebSocketShardUrl;

        var requestData = await _apiBase.RequestAsync(rawGetWebSocketShardUrl).ConfigureAwait(false);

        return requestData.ToObject<ShardWssInfo>();
    }
}