using MyBot.Api.Types;
namespace MyBot.Api.Raws
{
    /// <summary>
    /// 源Api信息 - 更新子频道信息
    /// </summary>
    public struct RawUpdateChannelApi : Base.IRawApiInfo
    {
        public string Version => "1.0";

        
        public string Url => "/channels/{channel_id}";

        public MethodType Method => MethodType.PATCH;
    }
}

