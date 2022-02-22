using System;
using QQChannelFramework.Api.Types;

namespace QQChannelFramework.Api.Raws
{
    /// <summary>
    /// 源Api信息 - 更新子频道信息
    /// </summary>
    public struct RawUpdateChannelApi : Base.IRawApiInfo
    {
        public string Version => "1.0";

        public bool NeedParam => true;

        public string Url => "/channels/{channel_id}";

        public MethodType Method => MethodType.PATCH;
    }
}

