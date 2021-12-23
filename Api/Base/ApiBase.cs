using System.Net.Http;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QQChannelFramework.Api.Types;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using QQChannelFramework.Exceptions;

namespace QQChannelFramework.Api.Base;

public class ApiBase
{
    private readonly string _releaseUrl = "https://api.sgroup.qq.com";

    private readonly string _sandBoxUrl = "https://sandbox.api.sgroup.qq.com";

    private readonly OpenApiAccessInfo _openApiAccessInfo;

    internal RequestMode _requestMode = RequestMode.Release;

    private static HttpClient _client;

    private object _rawContent;

    private HttpContent _content;

    public ApiBase(OpenApiAccessInfo openApiAccessInfo)
    {
        _openApiAccessInfo = openApiAccessInfo;

        if (_client is null)
        {
            _client = new HttpClient();
            _client.Timeout = TimeSpan.FromSeconds(10);
        }

        if (_openApiAccessInfo.BotAppId is null || _openApiAccessInfo.BotSecret is null || _openApiAccessInfo.BotToken is null)
        {
            throw new Exceptions.MissingAccessInfoException();
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
        _rawContent = obj;
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
            throw new Exceptions.MissingDataException();
        }

        string _requestUrl = _requestMode == RequestMode.Release ? _releaseUrl : _sandBoxUrl;

        _requestUrl = $"{_requestUrl}{api}";

        HttpResponseMessage responseMessage = null;

        switch (rawInfo.Method)
        {
            case MethodType.GET:

                if(_rawContent is not null)
                {
                    _requestUrl = $"{_requestUrl}?";
                    foreach (var item in (Dictionary<string, object>)_rawContent)
                    {
                        _requestUrl += $"{item.Key}={item.Value}&";
                    }
                }

                responseMessage = _client.GetAsync(_requestUrl).Result;

                break;

            case MethodType.POST:

                responseMessage = _client.PostAsync(_requestUrl, _content).Result;

                break;

            case MethodType.DELETE:

                responseMessage = _client.DeleteAsync(_requestUrl).Result;

                break;

            case MethodType.PATCH:

                responseMessage = _client.PatchAsync(_requestUrl, _content).Result;

                break;

            case MethodType.PUT:

                responseMessage = _client.PutAsync(_requestUrl, _content).Result;

                break;
        }

        // Console.WriteLine(await responseMessage.Content.ReadAsStringAsync());

        // 检查Http状态码
        InspectionHttpCode(responseMessage);

        // 状态码为204时无Content,无须读取
        if (responseMessage.StatusCode == (System.Net.HttpStatusCode)204)
        {
            return null;
        }

        var responseData = await responseMessage.Content.ReadAsStringAsync();

        var jsonObject = JToken.Parse(responseData);

        _rawContent = null;
        _content = null;

        if (jsonObject.ToString().StartsWith('['))
        {
            return jsonObject;
        }

        bool isError = false;

        try
        {
            var test = jsonObject["code"];

            if (test is not null)
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
        if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
        {
            return;
        }

        switch (httpResponseMessage.StatusCode)
        {
            case System.Net.HttpStatusCode.Unauthorized:

                throw new Exceptions.AccessInfoErrorException();

            case System.Net.HttpStatusCode.TooManyRequests:

                throw new Exceptions.RequestRateTooHighException() ;

            case System.Net.HttpStatusCode.NotFound:

                throw new Exceptions.ApiNotExistException();
        }
    }

    /// <summary>
    /// 检查请求结果Code
    /// </summary>
    /// <param name="resultData"></param>
    private void InspectionResultCode(in JToken resultData)
    {
        var code = int.Parse(resultData["code"].ToString());

        if (code >= 1000000 && code <= 2999999)
        {
            throw new SendMessageErrorException();
        }

        throw new ErrorResultException(code, resultData["message"].ToString());
    }
}