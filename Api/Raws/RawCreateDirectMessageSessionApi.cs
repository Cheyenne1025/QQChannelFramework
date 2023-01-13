using MyBot.Api.Types;

namespace MyBot.Api.Raws; 

public struct RawCreateDirectMessageSessionApi  : Base.IRawApiInfo
{
    public string Version => "1.0";

    
    public string Url => "/users/@me/dms";

    public MethodType Method => MethodType.POST;
}