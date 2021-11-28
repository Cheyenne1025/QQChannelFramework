using System;
using System.Collections.Generic;
using ChannelModels.Types;

namespace QQChannelFramework.Models
{
    /// <summary>
    /// 成员对象
    /// </summary>
    public class Member
    {
        /// <summary>
        /// 用户基础信息
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// 频道内昵称
        /// </summary>
        public string Nick { get; set; }

        /// <summary>
        /// 用户在频道内的身份组ID
        /// </summary>
        public List<string> Roles { get; set; }

        /// <summary>
        /// 用户加入频道的时间
        /// </summary>
        public DateTime JoinedAt { get; set; }
    }
}

