using System;
using System.Threading.Tasks;
using QQChannelFramework.Api.Base;
using QQChannelFramework.Models;
using QQChannelFramework.Api.Raws;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace QQChannelFramework.Api
{
    sealed partial class QQChannelApi
    {
        private static UserApi _userApi;

        public UserApi GetUserApi()
        {
            if (_userApi is null)
            {
                _userApi = new(apiBase);
            }

            return _userApi;
        }
    }

    /// <summary>
    /// 用户Api
    /// </summary>
    public class UserApi
    {
        readonly ApiBase _apiBase;

        public UserApi(ApiBase apiBase)
        {
            _apiBase = apiBase;
        }

        /// <summary>
        /// 获取当前用户信息
        /// </summary>
        /// <returns>当前用户信息</returns>
        public async Task<User> GetCurrentUser()
        {
            RawGetCurrentUserApi rawGetCurrentUserApi;

            var requestData = await _apiBase.RequestAsync(rawGetCurrentUserApi);

            User user = new User()
            {
                Id = requestData["id"].ToString(),
                UserName = requestData["username"].ToString(),
                Avatar = requestData["avatar"].ToString(),
                UnionOpenid = requestData["union_openid"].ToString(),
                UnionUserAccount = requestData["union_user_account"].ToString()
            };

            return user;
        }

        /// <summary>
        /// 获取当前用户加入的频道列表
        /// </summary>
        /// <param name="before">频道guild_id 读此id之前的数据 (之前之后只能选一个)</param>
        /// <param name="after">频道guild_id 读此id之后的数据 (之前之后只能选一个)</param>
        /// <param name="limit">每次拉取多少条数据 默认100 最大100</param>
        /// <returns>元组 (用户加入的频道列表,数量)</returns>
        public async Task<(List<Guild>, int)> GetJoinedChannels(string before = "", string after = "", int limit = 100)
        {
            if(before is not "" && after is not "")
            {
                throw new Exception("仅能读取此id之前或之后的数据 (before 或 after)");
            }

            if(limit < 1 || limit > 100)
            {
                limit = 100;
            }

            RawGetCurrentChannelsJoinedApi rawGetCurrentChannelsJoinedApi;

            var requestData = await _apiBase
                .WithData(new Dictionary<string,object>()
                {
                    {"before",before },
                    {"after",after },
                    {"limit",limit }
                })
                .RequestAsync(rawGetCurrentChannelsJoinedApi);

            var channelArray = JArray.Parse(requestData.ToString());

            List<Guild> guilds = new();

            foreach (var channelInfo in channelArray)
            {
                guilds.Add(new()
                {
                    Id = channelInfo["id"].ToString(),
                    Name = channelInfo["name"].ToString(),
                    Icon = channelInfo["icon"].ToString(),
                    Owner = bool.Parse(channelInfo["owner"].ToString())
                });
            }

            return (guilds, guilds.Count);
        }
    }
}

