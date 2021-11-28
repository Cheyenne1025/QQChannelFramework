using System;
namespace QQChannelFramework.Models
{
    /// <summary>
    /// 用户对象
    /// </summary>
    public class User
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 头像Url
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// 是否是机器人
        /// </summary>
        public bool IsBot { get; set; }

        /// <summary>
        /// 特殊关联应用的 openid，需要特殊申请并配置后才会返回。如需申请，请联系平台运营人员
        /// </summary>
        public string UnionOpenid { get; set; }

        /// <summary>
        /// 机器人关联的互联应用的用户信息，与union_openid关联的应用是同一个。如需申请，请联系平台运营人员
        /// </summary>
        public string UnionUserAccount { get; set; }
    }
}

