using MyBot.Api.Types;
namespace MyBot.Api.Raws
{
    /// <summary>
    /// 源Api信息 - 删除频道成员
    /// </summary>
    public struct RawDeleteChannelMemberApi : Base.IRawApiInfo
    {
        public string Version => "1.0";

        
        public string Url => "/guilds/{guild_id}/members/{user_id}";

        public MethodType Method => MethodType.DELETE;
    }
}

