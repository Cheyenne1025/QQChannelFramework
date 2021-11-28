using System;
using QQChannelFramework.Api.Types;

namespace QQChannelFramework.Api.Raws
{
    /// <summary>
    /// 源Api信息 - 获取频道中某成员信息
    /// </summary>
    public struct RawGetMemberInfoApi : Base.IRawApiInfo
    {
        public string Version => "1.0";

        public bool NeedParam => false;

        public string Url => "/guilds/{guild_id}/members/{user_id}";

        public MethodType Method => MethodType.GET;
    }
}

