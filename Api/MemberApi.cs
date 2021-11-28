using System;
using System.Threading.Tasks;
using QQChannelFramework.Api.Base;
using QQChannelFramework.Models;
using QQChannelFramework.Api.Raws;
using System.Collections.Generic;
using QQChannelFramework.Api.Types;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace QQChannelFramework.Api;

sealed partial class QQChannelApi
{
    private static MemberApi _memberApi;

    public MemberApi GetMemberApi()
    {
        if (_memberApi is null)
        {
            _memberApi = new(apiBase);
        }

        return _memberApi;
    }
}

/// <summary>
/// 成员Api
/// </summary>
public class MemberApi
{
    private readonly ApiBase _apiBase;

    public MemberApi(ApiBase apiBase)
    {
        _apiBase = apiBase;
    }

    /// <summary>
    /// 获取某个成员信息
    /// </summary>
    /// <returns>成员信息/returns>
    public async Task<Member> GetInfoAsync(string guild_id, string user_id)
    {
        RawGetMemberInfoApi rawGetMemberInfoApi;

        var precessedInfo = ApiFactory.Process(rawGetMemberInfoApi, new Dictionary<ParamType, string>()
            {
                {ParamType.guild_id,guild_id },
                {ParamType.user_id,user_id }
            });

        var requestData = await _apiBase.RequestAsync(precessedInfo);

        Member member = new()
        {
            User = new()
            {
                Id = requestData["user"]["id"].ToString(),
                UserName = requestData["user"]["username"].ToString(),
                Avatar = requestData["user"]["avatar"].ToString(),
                IsBot = bool.Parse(requestData["user"]["bot"].ToString()),
                UnionOpenid = requestData["user"]["union_openid"].ToString(),
                UnionUserAccount = requestData["user"]["union_user_account"].ToString()
            },
            Nick = requestData["nick"].ToString(),
            Roles = new(),
            JoinedAt = DateTime.Parse(requestData["joined_at"].ToString())
        };

        var roles = requestData["roles"].ToList();

        foreach (var roleId in roles)
        {
            member.Roles.Add(roleId.ToString());
        }

        return member;
    }
}