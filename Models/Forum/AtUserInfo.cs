using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QQChannelFramework.Models.Forum
{
    /// <summary>
    /// @用户信息
    /// </summary>
    public class AtUserInfo
    {
        /// <summary>
        /// 身份组Id
        /// </summary>
        [Newtonsoft.Json.JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        [Newtonsoft.Json.JsonProperty("nick")]
        public string Nick { get; set; }
    }
}
