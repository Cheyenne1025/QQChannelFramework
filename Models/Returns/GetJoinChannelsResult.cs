using System;
namespace ChannelModels.Returns
{
    /// <summary>
    /// 获机器人加入频道
    /// </summary>
    public class GetJoinChannelsResult
    {
        /// <summary>
        /// 频道Guild
        /// </summary>
        public string Guild { get; set; }

        /// <summary>
        /// 频道名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 频道头像Url
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 是否为拥有者
        /// </summary>
        public bool Owner { get; set; }
    }
}

