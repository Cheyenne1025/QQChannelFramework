using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MajsoulBot.Utils;
using MyBot.Tools;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QQChannelFramework.Tools;

/// <summary>
/// OpenApi接入信息
/// https://q.qq.com/qqbot/#/developer/developer-setting
/// </summary>
public class OpenApiAccessInfo {
   /// <summary>
   /// 机器人QQ号
   /// </summary>
   public string BotQQ { get; init; }

   /// <summary>
   /// 机器人开发识别码
   /// </summary>
   public string BotAppId { get; init; }

   /// <summary>
   /// 机器人Token
   /// </summary>
   public string BotToken { get; init; }

   /// <summary>
   /// 机器人密钥
   /// </summary>
   public string BotSecret { get; init; }

   /// <summary>
   /// 机器人密钥
   /// </summary>
   public OpenApiAccessInfo() {
      _refreshToken = new TimerTask(async () => {
         await GetAuthorization();
      }, TimeSpan.FromSeconds(20));
   }

   internal void Validate() {
      if (string.IsNullOrWhiteSpace(BotQQ) || string.IsNullOrWhiteSpace(BotSecret) ||
          string.IsNullOrWhiteSpace(BotToken) ||
          string.IsNullOrWhiteSpace(BotAppId)) {
         throw new InvalidOperationException("缺少鉴权信息，请参考 https://q.qq.com/qqbot/#/developer/developer-setting 初始化所有字段");
      }
   }

   private TimerTask _refreshToken;
   private readonly AsyncLock _refreshLock = new();
   private DateTime _refreshExpire = DateTime.Now;
   private string _auth;

   public async Task<string> GetAuthorization() {
      using var _ = await _refreshLock.LockAsync();
      if (DateTime.Now.AddSeconds(60) >= _refreshExpire && _auth != null) {
         return _auth;
      }

      try {
         using var http = new HttpClient();
         var resp = await http.PostAsync("https://bots.qq.com/app/getAppAccessToken", new StringContent(JsonConvert.SerializeObject(new {
            appId = BotAppId,
            clientSecret = BotSecret
         }), Encoding.UTF8, "application/json"));

         var json = JObject.Parse(await resp.Content.ReadAsStringAsync());
         _auth = json["access_token"]!.Value<string>();

         var expire = json["expires_in"]!.Value<int>();
         _refreshExpire = DateTime.Now.AddSeconds(expire);
         return _auth;
      } catch (Exception ex) {
         BotLog.Log(ex);
         return null;
      }
   }
}
