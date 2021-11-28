using System;
using QQChannelFramework.Api.Types;

namespace QQChannelFramework.Api.Raws
{
    /// <summary>
    /// 源Api信息 - 发送消息
    /// </summary>
    public struct RawSendMessageApi : Base.IRawApiInfo
    {
        public string Version => "1.0";

        public bool NeedParam => true;

        public string Url => "/channels/{channel_id}/messages";

        public MethodType Method => MethodType.POST;
    }
}