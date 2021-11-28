using System;
namespace ChannelModels.Types
{
    /// <summary>
    /// 播放状态
    /// </summary>
    public enum AudioStatus
    {
        /// <summary>
        /// 开始播放操作
        /// </summary>
        Start,
        /// <summary>
        /// 暂停播放操作
        /// </summary>
        Pause,
        /// <summary>
        /// 继续播放操作
        /// </summary>
        Resume,
        /// <summary>
        /// 停止播放操作
        /// </summary>
        Stop
    }
}

