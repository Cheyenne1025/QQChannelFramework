using System;
using QQChannelFramework.Api.Types;

namespace QQChannelFramework.Api.Raws
{
    /// <summary>
    /// 源Api信息 - 改指定子频道的权限
    /// </summary>
    public struct RawUpdateChildChannelPermissionsApi : Base.IRawApiInfo
    {
        public string Version => "1.0";

        public bool NeedParam => true;

        public string Url => "/channels/{channel_id}/members/{user_id}/permissions";

        public MethodType Method => MethodType.PUT;
    }
}

