using System.Collections.Generic;
using System.Threading.Tasks;
using MyBot.Api.Base;
using MyBot.Api.Raws;
using MyBot.Models;
namespace MyBot.Api;

sealed partial class QQChannelApi
{ 
    public GuildApi GetGuildApi()
    {
        return new(apiBase);
    }
}

/// <summary>
/// 频道Api
/// </summary>
public class GuildApi
{
    private readonly ApiBase _apiBase;

    public GuildApi(ApiBase apiBase)
    {
        _apiBase = apiBase;
    }

    /// <summary>
    /// 获取主频道信息
    /// </summary>
    public async Task<Guild> GetInfoAsync(string guild_id)
    {
        RawGetMainChannelApi rawGetMainChannelApi;

        var processedApiInfo = ApiFactory.Process(rawGetMainChannelApi, new Dictionary<Types.ParamType, string>()
            {
                {Types.ParamType.guild_id, guild_id }
            });

        var info = await _apiBase.RequestAsync(processedApiInfo).ConfigureAwait(false); 

        return info.ToObject<Guild>();
    }
}