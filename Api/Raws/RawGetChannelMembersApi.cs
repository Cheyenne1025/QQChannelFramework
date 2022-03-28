using System;
using QQChannelFramework.Api.Types;

namespace QQChannelFramework.Api.Raws
{
    /// <summary>
    /// 源Api信息 - 获取频道成员列表
    /// </summary>
    public struct RawGetChannelMembersApi : Base.IRawApiInfo
    {
        public string Version => "1.0";

        
        public string Url => "/guilds/{guild_id}/members";

        public MethodType Method => MethodType.GET;
    }
}

