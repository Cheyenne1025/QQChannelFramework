using MyBot.Api.Types;

namespace MyBot.Api.Raws
{
    /// <summary>
    /// 源Api信息 - 创建频道公告
    /// </summary>
    public struct RawCreateAnnounceApi : Base.IRawApiInfo
    {
        public string Version => "1.0";

        
        public string Url => "/guilds/{guild_id}/announces";

        public MethodType Method => MethodType.POST;
    }
}

