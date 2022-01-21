using QQChannelFramework.Api.Types;

namespace QQChannelFramework.Api.Raws; 

public struct RawCreateDirectMessageSessionApi  : Base.IRawApiInfo
{
    public string Version => "1.0";

    public bool NeedParam => true;

    public string Url => "/users/@me/dms";

    public MethodType Method => MethodType.POST;
}