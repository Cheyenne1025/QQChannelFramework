using MyBot.Api.Types;
namespace MyBot.Api.Raws
{
    /// <summary>
    /// 源Api信息 - 删除子频道公告
    /// </summary>
    public struct RawDeleteAnnounceApi : Base.IRawApiInfo
    {
        public string Version => "1.0";

        
        public string Url => "/guilds/{guild_id}/announces/{message_id}";

        public MethodType Method => MethodType.DELETE;
    }
}

