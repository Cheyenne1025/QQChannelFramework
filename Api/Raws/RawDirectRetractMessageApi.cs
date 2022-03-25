using QQChannelFramework.Api.Types;

namespace QQChannelFramework.Api.Raws; 

public struct RawDirectRetractMessageApi  : Base.IRawApiInfo
{
    public string Version => "1.0";

    public bool NeedParam => true;

    public string Url => "/dms/{guild_id}/messages/{message_id}";

    public MethodType Method => MethodType.DELETE;
}