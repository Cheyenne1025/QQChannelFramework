using System;
using QQChannelFramework.Api.Types;

namespace QQChannelFramework.Api.Raws
{
    /// <summary>
    /// 源Api信息 - 删除子频道
    /// </summary>
    public struct RawDeleteChannelApi : Base.IRawApiInfo
    {
        public string Version => "1.0";

        public bool NeedParam => false;

        public string Url => "/channels/{channel_id}";

        public MethodType Method => MethodType.DELETE;
    }
}

