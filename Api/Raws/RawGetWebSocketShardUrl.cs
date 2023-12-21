using MyBot.Api.Types;
namespace MyBot.Api.Raws;

/// <summary>
/// 源Api信息 - 获取带分片 WSS 接入点
/// </summary>
public struct RawGetWebSocketShardUrl : Base.IRawApiInfo
{
    public string Version => "1.0";

    
    public string Url => "/gateway/bot";

    public MethodType Method => MethodType.GET;
}