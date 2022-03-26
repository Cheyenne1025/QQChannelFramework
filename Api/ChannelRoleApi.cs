using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QQChannelFramework.Api.Base;
using QQChannelFramework.Api.Raws;
using QQChannelFramework.Api.Types;
using QQChannelFramework.Models;
using QQChannelFramework.Models.ParamModels;
using QQChannelFramework.Models.RawObject;

namespace QQChannelFramework.Api;

sealed partial class QQChannelApi {
    public ChannelRoleApi GetChannelRoleApi() {
        return new(apiBase);
    }
}

/// <summary>
/// 频道身份组Api
/// </summary>
public class ChannelRoleApi {
    private readonly ApiBase _apiBase;

    public ChannelRoleApi(ApiBase apiBase) {
        _apiBase = apiBase;
    }

    /// <summary>
    /// 获取所有频道身份组
    /// </summary>
    /// <returns>元组 (身份组列表,身份组当前数量,身份组上限)</returns>
    public async Task<(List<Role> Roles, int RoleNumLimit)> GetRolesAsync(string guild_id) {
        RawGetChannelRolesApi rawGetChannelRolesApi;

        var prcocessedInfo = ApiFactory.Process(rawGetChannelRolesApi, new Dictionary<ParamType, string>() {
            {ParamType.guild_id, guild_id}
        });

        var requestData = await _apiBase.RequestAsync(prcocessedInfo).ConfigureAwait(false);

        List<Role> roles = new List<Role>();

        JArray rolesData = JArray.Parse(requestData["roles"].ToString());

        foreach (var roleInfo in rolesData) {
            roles.Add(new() {
                Id = roleInfo["id"].ToString(),
                Color = uint.Parse(roleInfo["color"].ToString()),
                Name = roleInfo["name"].ToString(),
                Hoist = roleInfo["hoist"].ToString() is "1" ? true : false,
                Number = uint.Parse(roleInfo["number"].ToString()),
                MemberLimit = uint.Parse(roleInfo["member_limit"].ToString())
            });
        }

        return (roles, requestData["role_num_limit"].Value<int>());
    }

    /// <summary>
    /// 创建频道身份组
    /// </summary> 
    /// <param name="roleInfo">身份组属性</param>
    /// <param name="guild_id"></param>
    /// <returns>创建的身份组ID</returns>
    public async Task<Role> CreateAsync(RoleInfo roleInfo, string guild_id) {
        RawCreateChannelRoleApi rawCreateChannelRoleApi;

        var prcessedInfo = ApiFactory.Process(rawCreateChannelRoleApi, new Dictionary<ParamType, string>() {
            {ParamType.guild_id, guild_id}
        });

        var requestData = await _apiBase
            .WithContentData(new Dictionary<string, object>() {
                {"name", roleInfo.Name},
                {"color", string.IsNullOrWhiteSpace(roleInfo.Color) ? null : Tools.ConvertHelper.GetHex(roleInfo.Color)},
                {"hoist", roleInfo.Hoist.HasValue ? roleInfo.Hoist.Value ? 1 : 0 : null}
            }).RequestAsync(prcessedInfo).ConfigureAwait(false);

        return requestData["role"].ToObject<Role>();
    }

    /// <summary>
    /// 更新频道身份组
    /// </summary> 
    /// <param name="roleInfo">更新内容，不需要更新的设为null</param>
    /// <param name="guild_id">频道Guild</param>
    /// <param name="role_id">身份组ID</param>
    /// <returns>元组 (频道ID,身份组ID)</returns>
    public async Task<Role> UpdateInfoAsync(RoleInfo roleInfo, string guild_id,
        string role_id) {
        RawUpdateChannelRoleInfoApi rawUpdateChannelRoleInfoApi;

        var processedInfo = ApiFactory.Process(rawUpdateChannelRoleInfoApi, new Dictionary<ParamType, string>() {
            {ParamType.guild_id, guild_id},
            {ParamType.role_id, role_id}
        });

        var requestData = await _apiBase
            .WithContentData(new Dictionary<string, object>() {
                {"name", roleInfo.Name},
                {"color", string.IsNullOrWhiteSpace(roleInfo.Color) ? null : Tools.ConvertHelper.GetHex(roleInfo.Color)},
                {"hoist", roleInfo.Hoist.HasValue ? roleInfo.Hoist.Value ? 1 : 0 : null}
            }).RequestAsync(processedInfo).ConfigureAwait(false);

        return requestData["role"].ToObject<Role>();
    }

    /// <summary>
    /// 删除频道身份组
    /// </summary>
    /// <param name="guild_id">频道Guild</param>
    /// <param name="role_id">身份组ID</param>
    /// <returns>是否删除成功</returns>
    public async Task DeleteAsync(string guild_id, string role_id) {
        RawDeleteChannelRoleApi rawDeleteChannelRoleApi;

        var processedInfo = ApiFactory.Process(rawDeleteChannelRoleApi, new Dictionary<ParamType, string>() {
            {ParamType.guild_id, guild_id},
            {ParamType.role_id, role_id}
        });

        var requestData = await _apiBase.RequestAsync(processedInfo).ConfigureAwait(false);
    }

    /// <summary>
    /// 增加频道身份组成员
    /// <para>注: 如果要从身份组是 「子频道管理员」增加成员, 需要传递channelId参数，指定添加到哪个子频道</para>
    /// </summary>
    /// <param name="guild_id">频道Guild</param>
    /// <param name="user_id">成员ID</param>
    /// <param name="role_id">身份组ID</param>
    /// <param name="channelId">子频道ID</param>
    /// <returns></returns>
    public async Task<bool> AddMemberAsync(string guild_id, string user_id, string role_id, string channelId = null) {
        if (role_id is "5" && channelId is "") {
            throw new Exceptions.ParamErrorException("将成员添加到 「子频道管理员」身份组时需要传入第4个子频道ID参数");
        }

        RawAddMemberToRoleApi rawAddMemberToRoleApi;

        var processedInfo = ApiFactory.Process(rawAddMemberToRoleApi, new Dictionary<ParamType, string>() {
            {ParamType.guild_id, guild_id},
            {ParamType.user_id, user_id},
            {ParamType.role_id, role_id}
        });

        JToken requestData = null;

        requestData = await _apiBase
            .WithContentData(new {channel = new {id = channelId}})
            .RequestAsync(processedInfo)
            .ConfigureAwait(false);

        return requestData is null;
    }

    /// <summary>
    /// 删除频道身份组成员
    /// <para>注: 如果要从身份组是 「子频道管理员」删除成员, 需要传递channelId参数，指定添加到哪个子频道</para>
    /// </summary>
    /// <param name="guild_id">频道Guild</param>
    /// <param name="user_id">成员ID</param>
    /// <param name="role_id">身份组ID</param>
    /// <param name="channelId">子频道ID</param>
    /// <returns></returns>
    public async Task<bool>
        DeleteMemberAsync(string guild_id, string user_id, string role_id, string channelId = null) {
        if (role_id is "5" && channelId is "") {
            throw new Exceptions.ParamErrorException("将成员从「子频道管理员」身份组删除时需要传入第4个子频道ID参数");
        }

        RawDeleteMemberFromRoleApi rawDeleteMemberFromRoleApi;

        var processedInfo = ApiFactory.Process(rawDeleteMemberFromRoleApi, new Dictionary<ParamType, string>() {
            {ParamType.guild_id, guild_id},
            {ParamType.user_id, user_id},
            {ParamType.role_id, role_id}
        });

        JToken requestData = null;

        requestData = await _apiBase
            .WithContentData(new {channel = new {id = channelId}})
            .RequestAsync(processedInfo)
            .ConfigureAwait(false);

        return requestData is null;
    }
}