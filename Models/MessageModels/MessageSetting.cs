using System.Collections.Generic;
using System.Collections.Specialized;
using Newtonsoft.Json;

namespace QQChannelFramework.Models.MessageModels;

/// <summary>
/// 频道消息频率设置对象
/// </summary>
public class MessageSetting
{
    /// <summary>
    /// 是否允许创建私信
    /// </summary>
    [JsonProperty("disable_create_dm")]
    public string DisableCreateDm { get; set; }
    
    /// <summary>
    /// 是否允许发主动消息
    /// </summary>
    [JsonProperty("disable_push_msg")]
    public string DisablePushMsg { get; set; }
    
    /// <summary>
    /// 子频道 id 数组
    /// </summary>
    [JsonProperty("channel_ids")]
    public StringCollection ChannelIds { get; set; }
    
    /// <summary>
    /// 每个子频道允许主动推送消息最大消息条数
    /// </summary>
    [JsonProperty("channel_push_max_num")]
    public int ChannelPushMaxNum { get; set; }
}
