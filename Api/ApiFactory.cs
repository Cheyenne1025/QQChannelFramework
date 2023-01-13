using System.Collections.Generic;
using MyBot.Api.Base;

namespace MyBot.Api;

/// <summary>
/// Api加工厂
/// </summary>
public static class ApiFactory
{
    /// <summary>
    /// 加工Api
    /// </summary>
    /// <param name="rawApi"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static ProcessedApiInfo Process(IRawApiInfo rawApi, IDictionary<Types.ParamType, string> param)
    {
        ProcessedApiInfo processedApi = new();

        processedApi.Url = rawApi.Url;
        processedApi.RawInfo = rawApi;

        foreach (var key in param.Keys)
        {
            var replaceType = "{" + key + "}";
            if (processedApi.Url.Contains(replaceType))
            {
                processedApi.Url = processedApi.Url.Replace(replaceType, param[key]);
            }
        }

        return processedApi;
    }
}