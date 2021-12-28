using QQChannelFramework.Api.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QQChannelFramework.Api.Raws;

/// <summary>
/// 源Api信息 - 撤回消息
/// </summary>
public struct RawRetractMessageApi : Base.IRawApiInfo
{
    public string Version => "1.0";

    public bool NeedParam => false;

    public string Url => "/channels/{channel_id}/messages/{message_id}";

    public MethodType Method => MethodType.DELETE;
}