using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QQChannelFramework.Api.Types;
using QQChannelFramework.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace QQChannelFramework.Api.Base;

public class ApiBase {
    private readonly string _releaseUrl = "https://api.sgroup.qq.com";

    private readonly string _sandBoxUrl = "https://sandbox.api.sgroup.qq.com";

    private readonly OpenApiAccessInfo _openApiAccessInfo;

    internal RequestMode _requestMode = RequestMode.Release;

    private static HttpClient _client;

    private string _queryParam;

    private HttpContent _content;

    public ApiBase(OpenApiAccessInfo openApiAccessInfo) {
        _openApiAccessInfo = openApiAccessInfo;

        if (_client is null) {
            _client = new HttpClient();
            _client.Timeout = TimeSpan.FromSeconds(10);
        }

        if (_openApiAccessInfo.BotAppId is null || _openApiAccessInfo.BotSecret is null ||
            _openApiAccessInfo.BotToken is null) {
            throw new Exceptions.MissingAccessInfoException();
        }
    }

    /// <summary>
    /// 使用Bot身份
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public ApiBase UseBotIdentity() {
        _client.DefaultRequestHeaders.Authorization =
            new("Bot", $"{_openApiAccessInfo.BotAppId}.{_openApiAccessInfo.BotToken}");

        return this;
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
            .Where(a => !string.IsNullOrWhiteSpace(a.Value.ToString()))
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

    private async Task<JToken> RequestAsync(string api, IRawApiInfo rawInfo) { 

        string _requestUrl = _requestMode == RequestMode.Release ? _releaseUrl : _sandBoxUrl;

        _requestUrl = $"{_requestUrl}{api}";

        HttpResponseMessage responseMessage = null; 

        if (!string.IsNullOrWhiteSpace(_queryParam)) {
            _requestUrl = $"{_requestUrl}?{_queryParam}";
        }  
        
        var req = new HttpRequestMessage(rawInfo.Method.ToHttpMethod(), _requestUrl);
        if (req.Method != HttpMethod.Get)
            req.Content = _content;
        responseMessage = await _client.SendAsync(req).ConfigureAwait(false);
          
        var traceId = "Missing";
        if (responseMessage.Headers.TryGetValues("X-Tps-Trace-Id", out var val)) {
            traceId = val.FirstOrDefault();
        }

        // 检查Http状态码
        InspectionHttpCode(responseMessage, traceId);

        // 状态码为204时无Content,无须读取
        if (responseMessage.StatusCode == (System.Net.HttpStatusCode) 204) {
            return null;
        }

        var responseData = await responseMessage.Content.ReadAsStringAsync();

        var jsonObject = JToken.Parse(responseData);

        _queryParam = null;
        _content = null;

        if (jsonObject.ToString().StartsWith('[')) {
            return jsonObject;
        }

        bool isError = false;

        try {
            var test = jsonObject["code"];

            if (test is not null) {
                isError = true;
            }
        } catch { }

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

        Exception exception = code switch
        {
            304023 or 304024 => new MessageAuditException(code, resultData["message"].ToString(), resultData["data"]["message_audit"]["audit_id"].ToString()),
            _=> new ErrorResultException(code, resultData["message"].ToString(), traceId)
        };

        throw exception;
    }
}