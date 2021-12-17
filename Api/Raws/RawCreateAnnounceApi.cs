using System;
using QQChannelFramework.Api.Types;

namespace QQChannelFramework.Api.Raws
{
    /// <summary>
    /// 源Api信息 - 创建子频道公告
    /// </summary>
    public struct RawCreateAnnounceApi : Base.IRawApiInfo
    {
        public string Version => "1.0";

        public bool NeedParam => true;

        public string Url => "/channels/{channel_id}/announces";

        public MethodType Method => MethodType.POST;
    }
}

