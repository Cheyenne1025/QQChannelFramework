using System.Collections.Generic;
using System.Threading.Tasks;
using ChannelModels;
using QQChannelFramework.Api.Base;
using QQChannelFramework.Api.Raws;
using QQChannelFramework.Models;

namespace QQChannelFramework.Api;

sealed partial class QQChannelApi
{ 
    public ChannelApi GetChannelApi()
    {
        return new(apiBase);
    }
}

/// <summary>
/// 频道Api
/// </summary>
public class ChannelApi
{
    private readonly ApiBase _apiBase;

    public ChannelApi(ApiBase apiBase)
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

        Guild guild = new()
        {
            Id = info["id"].ToString(),
            Name = info["name"].ToString(),
            OwnerId = info["owner_id"].ToString(),
            Description = info["description"].ToString(),
            Owner = info["owner"].ToString() is "1" ? true : false,
            MemberCount = int.Parse(info["member_count"].ToString()),
            MaxMembers = int.Parse(info["max_members"].ToString())
        };

        return guild;
    }
}