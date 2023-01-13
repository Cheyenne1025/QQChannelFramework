﻿using MyBot.Api.Types;

namespace MyBot.Api.Raws
{
    /// <summary>
    /// 源Api信息 - 改指定子频道的权限
    /// </summary>
    public struct RawUpdateChannelPermissionsApi : Base.IRawApiInfo
    {
        public string Version => "1.0";

        
        public string Url => "/channels/{channel_id}/members/{user_id}/permissions";

        public MethodType Method => MethodType.PUT;
    }
}

