using QQChannelFramework.Api.Types;

namespace QQChannelFramework.Api.Raws;

/// <summary>
/// 源Api信息 - 获取频道消息频率设置
/// </summary>
public struct RawGetMessageSettingInfoApi : Base.IRawApiInfo
{
    public string Version => "1.0";
    public string Url => "/guilds/{guild_id}/message/setting";
    public MethodType Method => MethodType.GET;
}
