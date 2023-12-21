using System.Collections.Generic;
using System.Threading.Tasks;
using MyBot.Api.Base;
using MyBot.Api.Raws;
using MyBot.Models.ChannelPermissionModels;
namespace MyBot.Api;

sealed partial class QQChannelApi
{
    public GuildPermissionApi GetGuildPermissionApi()
    {
        return new(apiBase);
    }
}

/// <summary>
/// 频道Api接口权限Api
/// </summary>
public class GuildPermissionApi
{
    private readonly ApiBase _apiBase;

    public GuildPermissionApi(ApiBase apiBase)
    {
        _apiBase = apiBase;
    }

    /// <summary>
    /// 获取频道可用权限列表
    /// </summary>
    /// <param name="guildId"></param>
    /// <returns></returns>
    public async Task<List<APIPermission>> GetPermissionsAsync(string guildId)
    {
        RawGetChannelPermissionApi rawGetChannelPermissionApi;

        var processedApiInfo = ApiFactory.Process(rawGetChannelPermissionApi, new Dictionary<Types.ParamType, string>()
        {
            [Types.ParamType.guild_id] = guildId
        });

        var info = await _apiBase.RequestAsync(processedApiInfo).ConfigureAwait(false);

        List<APIPermission> permissions = new();

        foreach (var json in info["apis"])
        {
            permissions.Add(json.ToObject<APIPermission>());
        }

        return permissions;
    }

    /// <summary>
    /// 创建频道 API 接口权限授权链接
    /// <br/>
    /// <b>每天只能在一个频道内发 3 条（默认值）频道权限授权链接</b>
    /// </summary>
    /// <param name="guildId"></param>
    /// <param name="channelId"></param>
    /// <param name="identifyInfo"></param>
    /// <param name="desc"></param>
    /// <returns></returns>
    public async Task<APIPermissionDemand> CreateAuthPermissionUrlAsync(string guildId, string channelId, APIPermissionDemandIdentify identifyInfo, string desc)
    {
        RawCreateChannelPermissionAuthUrlApi rawCreateChannelPermissionUrlApi;

        var processedApiInfo = ApiFactory.Process(rawCreateChannelPermissionUrlApi, new Dictionary<Types.ParamType, string>()
        {
            [Types.ParamType.guild_id] = guildId
        });

        var info = await _apiBase
            .WithJsonContentData(new Dictionary<string, object>()
            {
                ["channel_id"] = channelId,
                ["api_identify"] = identifyInfo,
                ["desc"] = desc
            })
            .RequestAsync(processedApiInfo).ConfigureAwait(false);

        return info.ToObject<APIPermissionDemand>();
    }
}