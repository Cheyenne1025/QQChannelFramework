using System;
using System.Net.Http;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QQChannelFramework.Api.Types;
using QQChannelFramework.Datas;
using System.Linq;
using QQChannelFramework.OfficialExceptions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QQChannelFramework.Api.Base
{
    public class ApiBase
    {
        private readonly string _releaseUrl = "https://api.sgroup.qq.com";

        private readonly string _sandBoxUrl = "https://sandbox.api.sgroup.qq.com";

        private readonly OpenApiAccessInfo _openApiAccessInfo;

        private RequestMode _requestMode = RequestMode.Release;

        private static Dictionary<int, string> _officialExceptions;

        private static HttpClient _client;

        private HttpContent _content;

        public ApiBase(OpenApiAccessInfo openApiAccessInfo)
        {
            _openApiAccessInfo = openApiAccessInfo;

            if (_client is null)
            {
                _officialExceptions = new Dictionary<int, string>();

                #region 反射存在的官方异常
                Assembly assembly = Assembly.GetExecutingAssembly();
                var types = assembly.GetTypes().Where(x => x.GetCustomAttribute(typeof(OfficialException)) is not null).ToList();

                foreach (var type in types)
                {
                    var attributInfo = (OfficialException)type.GetCustomAttribute(typeof(OfficialException));

                    if (_officialExceptions.ContainsKey(attributInfo.Code))
                    {
                        continue;
                    }

                    _officialExceptions.Add(attributInfo.Code, attributInfo.Message);
                }
                #endregion

                _client = new HttpClient();
            }

            if(_openApiAccessInfo.BotAppId is null || _openApiAccessInfo.BotSecret is null || _openApiAccessInfo.BotToken is null)
            {
                throw new Exception("OpenApi信息未填写完整");
            }
        }

        /// <summary>
        /// 使用Bot身份
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public ApiBase UseBotIdentity()
        {
            _client.DefaultRequestHeaders.Authorization = new("Bot", $"{_openApiAccessInfo.BotAppId}.{_openApiAccessInfo.BotToken}");

            return this;
        }

        /// <summary>
        /// 携带数据
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public ApiBase WithData(object obj)
        {
            _content = new StringContent(JsonConvert.SerializeObject(obj));
            _content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            return this;
        }

        /// <summary>
        /// 使用正式模式 (默认)
        /// </summary>
        /// <returns></returns>
        public ApiBase UseReleaseMode()
        {
            _requestMode = Types.RequestMode.Release;

            return this;
        }

        /// <summary>
        /// 使用沙箱模式
        /// </summary>
        /// <returns></returns>
        public ApiBase UseSandBoxMode()
        {
            _requestMode = Types.RequestMode.SandBox;

            return this;
        }

        private async Task<JToken> RequestAsync(string api, IRawApiInfo rawInfo)
        {
            if (rawInfo.NeedParam && _content is null)
            {
                throw new Exception("该API需要传递参数，请参阅开发文档中该API需求的参数，并使用WithData()方法传递");
            }

            string _requestUrl = _requestMode == RequestMode.Release ? _releaseUrl : _sandBoxUrl;

            _requestUrl = $"{_requestUrl}{api}";

            HttpResponseMessage responseMessage = null;

            switch (rawInfo.Method)
            {
                case MethodType.GET:

                    responseMessage = _client.GetAsync(_requestUrl).Result;

                    break;

                case MethodType.POST:

                    responseMessage =  _client.PostAsync(_requestUrl, _content).Result;

                    break;

                case MethodType.DELETE:

                    responseMessage =  _client.DeleteAsync(_requestUrl).Result;

                    break;

                case MethodType.PATCH:

                    responseMessage = _client.PatchAsync(_requestUrl, _content).Result;

                    break;

                case MethodType.PUT:

                    responseMessage =  _client.PutAsync(_requestUrl, _content).Result;

                    break;
            }

            // 检查Http状态码
            InspectionHttpCode(responseMessage);

            // 状态码为204时无Content,无须读取
            if(responseMessage.StatusCode == (System.Net.HttpStatusCode)204)
            {
                return null;
            }

            var responseData = await responseMessage.Content.ReadAsStringAsync();

            var jsonObject = JToken.Parse(responseData);

            _content = null;

            if (jsonObject.ToString().StartsWith('['))
            {
                return jsonObject;
            }

            bool isError = false;

            try
            {
                var test = jsonObject["code"];

                if(test is not null)
                {
                    isError = true;
                }
            }
            catch { }

            if (isError)
            {
                // 检查返回结果
                InspectionResultCode(jsonObject);
            }

            return jsonObject;
        }

        /// <summary>
        /// 使用源Api信息进行异步请求
        /// </summary>
        /// <param name="apiInfo"></param>
        /// <returns></returns>
        public async Task<JToken> RequestAsync(IRawApiInfo apiInfo)
        {
            return await RequestAsync(apiInfo.Url, apiInfo);
        }

        /// <summary>
        /// 使用处理过的Api信息进行异步请求
        /// </summary>
        /// <param name="apiInfo"></param>
        /// <returns></returns>
        public async Task<JToken> RequestAsync(ProcessedApiInfo apiInfo)
        {
            return await RequestAsync(apiInfo.Url, apiInfo.RawInfo);
        }

        /// <summary>
        /// 检查HttpCode
        /// </summary>
        /// <param name="httpResponseMessage"></param>
        private void InspectionHttpCode(in HttpResponseMessage httpResponseMessage)
        {
            if(httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return;
            }

            switch (httpResponseMessage.StatusCode)
            {
                case System.Net.HttpStatusCode.Unauthorized:

                    throw new Exception("鉴权信息有误");

                case System.Net.HttpStatusCode.TooManyRequests:

                    throw new Exception("请求频率过高");

                case System.Net.HttpStatusCode.NotFound:

                    throw new Exception("API不存在，请检查开发文档，确保API可用");
            }
        }

        /// <summary>
        /// 检查请求结果Code
        /// </summary>
        /// <param name="resultData"></param>
        private void InspectionResultCode(in JToken resultData)
        {
            var code = int.Parse(resultData["code"].ToString());

            Console.WriteLine($"错误代码 -> {code}");

            if(code >= 1000000 && code <= 2999999)
            {
                throw new Exception("发送消息错误");
            }

            if(_officialExceptions.ContainsKey(code))
            {
                throw new Exception(_officialExceptions[code]);
            }
        }
    }
}