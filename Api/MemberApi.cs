using System;
using System.Threading.Tasks;
using QQChannelFramework.Api.Base;
using QQChannelFramework.Models;
using QQChannelFramework.Api.Raws;
using System.Collections.Generic;
using QQChannelFramework.Api.Types;
using System.Linq;
using Newtonsoft.Json.Linq;
using QQChannelFramework.Datas;

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

        var requestData = await _apiBase.RequestAsync(precessedInfo).ConfigureAwait(false);

        var jObj = (JObject)requestData["user"];

        Member member = new()
        {
            User = new()
            {
                Id = requestData["user"]["id"].ToString(),
                UserName = requestData["user"]["username"].ToString(),
                Avatar = requestData["user"]["avatar"].ToString(),
                IsBot = bool.Parse(requestData["user"]["bot"].ToString()),
                UnionOpenid = jObj.ContainsKey("union_openid") ? requestData["user"]["union_openid"].ToString() : "",
                UnionUserAccount = jObj.ContainsKey("union_user_account") ? requestData["user"]["union_user_account"].ToString() : "",
            },
            Nick = requestData["nick"].ToString(),
            Roles = new(),
            JoinedAt = DateTime.Parse(requestData["joined_at"].ToString())
        };


        if (jObj.ContainsKey("roles"))
        {
            var roles = requestData["roles"].ToList();

            foreach (var roleId in roles)
            {
                member.Roles.Add(roleId.ToString());
            }
        }

        return member;
    }

    /// <summary>
    /// 获取频道内所有成员信息 (私域可用)
    /// </summary>
    /// <param name="guild_id">主频道GuildID</param>
    /// <param name="after">上一次回包中最大的用户ID， 如果是第一次请求填0，默认为0</param>
    /// <param name="limit">分页大小，1-1000，默认是1</param>
    /// <returns>元组 (成员集合，成员数量)</returns>
    /// <exception cref="Exceptions.BotNotIsPrivateException"></exception>
    public async Task<(List<Member>,int)> GetMembers(string guild_id,string after = "0", UInt32 limit = 1)
    {
        if(CommonState.PrivateBot is false)
        {
            throw new Exceptions.BotNotIsPrivateException();
        }

        if(limit > 1000)
        {
            limit = 1000;
        }

        RawGetChannelMembersApi rawGetChannelMembersApi;

        var processedInfo = ApiFactory.Process(rawGetChannelMembersApi, new Dictionary<ParamType, string>()
        {
            {ParamType.guild_id,guild_id }
        });

        var requestData = await _apiBase
            .WithData(new Dictionary<string,object>()
            {
                {"after", after},
                {"limit",limit }
            })
            .RequestAsync(processedInfo);

        JArray jArray = JArray.Parse(requestData.ToString());

        List<Member> members = new();

        foreach (var info in jArray)
        {
            var jObj = (JObject)info["user"];

            Member member = new()
            {
                User = new()
                {
                    Id = info["user"]["id"].ToString(),
                    UserName = info["user"]["username"].ToString(),
                    Avatar = info["user"]["avatar"].ToString(),
                    IsBot = bool.Parse(info["user"]["bot"].ToString()),
                    UnionOpenid = jObj.ContainsKey("union_openid") ? info["user"]["union_openid"].ToString() : "",
                    UnionUserAccount = jObj.ContainsKey("union_user_account") ? info["user"]["union_user_account"].ToString() : "",
                },
                Nick = info["nick"].ToString(),
                Roles = new(),
                JoinedAt = DateTime.Parse(info["joined_at"].ToString())
            };

            if (jObj.ContainsKey("roles"))
            {
                foreach (var role in (JArray)info["roles"])
                {
                    member.Roles.Add(role.ToString());
                }
            }

            members.Add(member);
        }

        return (members, members.Count);
    }

    /// <summary>
    /// 删除指定频道成员 (私域可用)
    /// </summary>
    /// <param name="guild_id"></param>
    /// <param name="user_id"></param>
    /// <returns></returns>
    public async Task<bool> DeleteMemberAsync(string guild_id,string user_id)
    {
        if (CommonState.PrivateBot is false)
        {
            throw new Exceptions.BotNotIsPrivateException();
        }

        RawDeleteChannelMemberApi rawDeleteChannelMemberApi;

        var processedInfo = ApiFactory.Process(rawDeleteChannelMemberApi, new Dictionary<ParamType, string>()
        {
            {ParamType.guild_id,guild_id },
            {ParamType.user_id,user_id }
        });

        var requestData = await _apiBase.RequestAsync(processedInfo).ConfigureAwait(false);

        return requestData is null;
    }
}