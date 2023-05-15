using System.Threading.Tasks;
using QQChannelFramework.Api.Base;
using QQChannelFramework.Models;
using QQChannelFramework.Api.Raws;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace QQChannelFramework.Api;

sealed partial class QQChannelApi {
   public UserApi GetUserApi() {
      return new(apiBase);
   }
}

/// <summary>
/// 用户Api
/// </summary>
public class UserApi {
   readonly ApiBase _apiBase;

   public UserApi(ApiBase apiBase) {
      _apiBase = apiBase;
   }

   /// <summary>
   /// 获取当前用户信息
   /// </summary>
   /// <returns>当前用户信息</returns>
   public async Task<User> GetCurrentUserAsync() {
      RawGetCurrentUserApi rawGetCurrentUserApi;

      var requestData = await _apiBase.RequestAsync(rawGetCurrentUserApi).ConfigureAwait(false);

      var jObj = (JObject) requestData;

      User user = new User() {
         Id = requestData["id"].ToString(),
         UserName = requestData["username"].ToString(),
         Avatar = requestData["avatar"].ToString(),
         UnionOpenid = jObj.ContainsKey("union_openid") ? requestData["union_openid"].ToString() : "",
         UnionUserAccount = jObj.ContainsKey("union_user_account") ? requestData["union_user_account"].ToString() : ""
      };

      return user;
   }

   /// <summary>
   /// 获取当前用户加入的频道列表
   /// </summary>
   /// <param name="before">频道guild_id 读此id之前的数据 (之前之后只能选一个)</param>
   /// <param name="after">频道guild_id 读此id之后的数据 (之前之后只能选一个)</param>
   /// <param name="limit">每次拉取多少条数据 默认100 最大100</param>
   /// <returns>用户加入的频道列表</returns>
   public async Task<List<Guild>> GetJoinedChannelsAsync(string before = null, string after = "", int limit = 100) {
      if (limit is < 1 or > 100) {
         limit = 100;
      }

      RawGetCurrentChannelsJoinedApi rawGetCurrentChannelsJoinedApi;

      var requestData = await _apiBase.WithQueryParam(new Dictionary<string, object>() {
         {"before", before},
         {"after", after},
         {"limit", limit}
      }).RequestAsync(rawGetCurrentChannelsJoinedApi).ConfigureAwait(false);

      var channelArray = JArray.Parse(requestData.ToString());

      return channelArray.Select(channelInfo => channelInfo.ToObject<Guild>()).ToList();
   }

   public async Task<List<Guild>> GetAllJoinedChannelsAsync() {
      List<Guild> guilds = new();

      string after = "";

      while (true) {
         var batch = await GetJoinedChannelsAsync(null, after, 100).ConfigureAwait(false);

         if (!batch.Any())
            break;

         guilds.AddRange(batch);

         after = batch.Last().Id;
      }

      return guilds;
   }
}