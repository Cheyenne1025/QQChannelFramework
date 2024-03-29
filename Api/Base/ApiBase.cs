﻿using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MyBot.Api.Types;
using MyBot.Datas;
using MyBot.Exceptions;
using MyBot.Tools;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace MyBot.Api.Base;

public class ApiBase {
   private const string ReleaseUrl = "https://api.sgroup.qq.com";

   private const string SandBoxUrl = "https://sandbox.api.sgroup.qq.com";

   private readonly OpenApiAccessInfo _openApiAccessInfo;

   internal RequestMode _requestMode = RequestMode.Release;

   private string _queryParam;

   private HttpContent _content;

   private static readonly HttpClient Client;

   static ApiBase() {
      Client = new HttpClient(new SocketsHttpHandler() {
         PooledConnectionLifetime = TimeSpan.FromMinutes(120)
      });
      Client.Timeout = TimeSpan.FromSeconds(10);
   }

   public ApiBase(OpenApiAccessInfo openApiAccessInfo) {
      _openApiAccessInfo = openApiAccessInfo;
      _openApiAccessInfo.Validate();
   }

   /// <summary>
   /// 携带body数据
   /// </summary>
   /// <param name="obj"></param>
   /// <returns></returns>
   public ApiBase WithJsonContentData(object obj) {
      _content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");

      return this;
   }

   /// <summary>
   /// 携带body数据
   /// </summary> 
   /// <param name="form"></param>
   /// <returns></returns>
   public ApiBase WithMultipartContentData(MultipartFormDataContent form) {
      _content = form;

      return this;
   }

   /// <summary>
   /// 携带query参数
   /// </summary>
   /// <param name="obj">字典</param>
   /// <returns></returns>
   public ApiBase WithQueryParam(Dictionary<string, object> obj) {
      _queryParam = string.Join('&', obj
         .Where(a => a.Value != null && !string.IsNullOrWhiteSpace(a.Value.ToString()))
         .Select(a => $"{a.Key}={a.Value}"));

      return this;
   }

   /// <summary>
   /// 携带query参数
   /// </summary>
   /// <param name="obj">匿名对象</param>
   /// <returns></returns>
   public ApiBase WithQueryParam(object obj) {
      var propertyInfos = obj.GetType().GetProperties();
      _queryParam = string.Join('&', propertyInfos
         .Select(a => (a.Name, a.GetValue(obj)))
         .Where(a => !string.IsNullOrWhiteSpace(a.Item2?.ToString())));

      return this;
   }

   /// <summary>
   /// 使用正式模式 (默认)
   /// </summary>
   /// <returns></returns>
   public ApiBase UseReleaseMode() {
      _requestMode = RequestMode.Release;

      return this;
   }

   /// <summary>
   /// 使用沙箱模式
   /// </summary>
   /// <returns></returns>
   public ApiBase UseSandBoxMode() {
      _requestMode = RequestMode.SandBox;

      return this;
   }

   public async Task<JToken> RequestAsync(string api, IRawApiInfo rawInfo) {
      return await RequestAsync(api, rawInfo.Method.ToHttpMethod());
   }

   public async Task<JToken> RequestAsync(string api, HttpMethod method) {

      string _requestUrl = _requestMode == RequestMode.Release ? ReleaseUrl : SandBoxUrl;

      _requestUrl = $"{_requestUrl}{api}";

      HttpResponseMessage responseMessage = null;

      if (!string.IsNullOrWhiteSpace(_queryParam)) {
         _requestUrl = $"{_requestUrl}?{_queryParam}";
      }

      var req = new HttpRequestMessage(method, _requestUrl);
      req.Headers.TryAddWithoutValidation("Authorization", $"QQBot {await _openApiAccessInfo.GetAuthorization()}");
      req.Headers.TryAddWithoutValidation("X-Union-Appid", _openApiAccessInfo.BotAppId);

      if (req.Method != HttpMethod.Get)
         req.Content = _content;
      responseMessage = await Client.SendAsync(req).ConfigureAwait(false);

      var traceId = "Missing";
      if (responseMessage.Headers.TryGetValues("X-Tps-Trace-Id", out var val)) {
         traceId = val.FirstOrDefault();
      }

      // 检查Http状态码
      InspectionHttpCode(responseMessage, traceId);

      // 状态码为204时无Content,无须读取
      if (responseMessage.StatusCode == (System.Net.HttpStatusCode)204) {
         return null;
      }

      var responseData = await responseMessage.Content.ReadAsStringAsync();

      var jsonObject = JToken.Parse(responseData);

      _queryParam = null;
      _content = null;

      if (responseData.StartsWith('[')) {
         return jsonObject;
      }

      bool isError = false;

      try {
         var test = jsonObject["code"];

         if (test is not null) {
            isError = true;
         }
      } catch (Exception e) {
         BotLog.Log(e);
      }

      if (isError) {
         // 检查返回结果
         InspectionResultCode(jsonObject, traceId);
      }

      return jsonObject;
   }

   /// <summary>
   /// 使用源Api信息进行异步请求
   /// </summary>
   /// <param name="apiInfo"></param>
   /// <returns></returns>
   public async Task<JToken> RequestAsync(IRawApiInfo apiInfo) {
      return await RequestAsync(apiInfo.Url, apiInfo);
   }

   /// <summary>
   /// 使用处理过的Api信息进行异步请求
   /// </summary>
   /// <param name="apiInfo"></param>
   /// <returns></returns>
   public async Task<JToken> RequestAsync(ProcessedApiInfo apiInfo) {
      return await RequestAsync(apiInfo.Url, apiInfo.RawInfo);
   }

   /// <summary>
   /// 检查HttpCode
   /// </summary>
   /// <param name="httpResponseMessage"></param>
   private void InspectionHttpCode(in HttpResponseMessage httpResponseMessage, string traceId) {
      if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK) {
         return;
      }

      switch (httpResponseMessage.StatusCode) {
         case System.Net.HttpStatusCode.Unauthorized:

            throw new HttpApiException(new AccessInfoErrorException(), traceId);

         case System.Net.HttpStatusCode.TooManyRequests:

            throw new HttpApiException(new RequestRateTooHighException(), traceId);

         case System.Net.HttpStatusCode.NotFound:

            throw new HttpApiException(new ApiNotExistException(), traceId);
      }
   }

   /// <summary>
   /// 检查请求结果Code
   /// </summary>
   /// <param name="resultData"></param>
   private void InspectionResultCode(in JToken resultData, string traceId) {
      var code = int.Parse(resultData["code"].ToString());

      Exception exception = code switch {
         304023 or 304024 => new MessageAuditException(code, resultData["message"].ToString(),
            resultData["data"]!["message_audit"]!["audit_id"]!.ToString()),
         _ => new ErrorResultException(code, resultData["message"].ToString(), traceId)
      };

      throw exception;
   }
}
