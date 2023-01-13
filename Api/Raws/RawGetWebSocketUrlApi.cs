using MyBot.Api.Types;

namespace MyBot.Api.Raws;

/// <summary>
/// 源Api信息 - 获取通用 WSS 接入点
/// </summary>
public struct RawGetWebSocketUrlApi : Base.IRawApiInfo
{
    public string Version => "1.0";

    
    public string Url => "/gateway";

    public MethodType Method => MethodType.GET;
}