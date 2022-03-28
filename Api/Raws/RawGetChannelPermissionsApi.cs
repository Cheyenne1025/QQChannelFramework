using System;
using QQChannelFramework.Api.Types;

namespace QQChannelFramework.Api.Raws
{
    /// <summary>
    /// 源Api信息 - 获取指定子频道的权限
    /// </summary>
    public struct RawGetChannelPermissionsApi : Base.IRawApiInfo
    {
        public string Version => "1.0";

        
        public string Url => "/channels/{channel_id}/members/{user_id}/permissions";

        public MethodType Method => MethodType.GET;
    }
}

