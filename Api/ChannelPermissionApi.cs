using System.Collections.Generic;
using System.Threading.Tasks;
using MyBot.Api.Base;
using MyBot.Api.Raws;
using MyBot.Api.Types;
using MyBot.Models;

namespace MyBot.Api
{
    sealed partial class QQChannelApi
    { 
        public ChannelPermissionApi GetChannelPermissionApi()
        {
            return new(apiBase);
        }
    }

    /// <summary>
    /// 子频道权限Api
    /// </summary>
    public class ChannelPermissionApi
    {
        readonly ApiBase _apiBase;

        public ChannelPermissionApi(ApiBase apiBase)
        {
            _apiBase = apiBase;
        }

        /// <summary>
        /// 获取用户在子频道的权限
        /// </summary>
        /// <param name="channel_id">子频道ID</param>
        /// <param name="user_id">用户ID</param>
        /// <returns>权限对象</returns>
        public async Task<ChannelPermissions> GetPermissionsAsync(string channel_id, string user_id)
        {
            RawGetChannelPermissionsApi rawGetChannelPermissionsApi;

            var processedInfo = ApiFactory.Process(rawGetChannelPermissionsApi, new Dictionary<ParamType, string>()
            {
                {ParamType.channel_id,channel_id },
                {ParamType.user_id,user_id }
            });

            var requestData = await _apiBase.RequestAsync(processedInfo).ConfigureAwait(false);

            return requestData.ToObject<ChannelPermissions>();
        }

        /// <summary>
        /// 获取指定子频道身份组的权限
        /// </summary>
        /// <param name="channelId">子频道ID</param>
        /// <param name="roleId">身份组ID</param>
        /// <returns></returns>
        public async Task<ChannelRolePermissions> GetChannelRolePermissionAsync(string channelId, string roleId)
        {
            RawGetChannelRolePermissionApi rawGetChannelRolePermissionApi;

            var processedInfo = ApiFactory.Process(rawGetChannelRolePermissionApi, new Dictionary<ParamType, string>()
            {
                {ParamType.channel_id,channelId},
                {ParamType.role_id,roleId}
            });

            var requestData = await _apiBase.RequestAsync(processedInfo).ConfigureAwait(false);

            return requestData.ToObject<ChannelRolePermissions>();
        }

        /// <summary>
        /// 修改指定子频道身份组的权限
        /// </summary>
        /// <param name="channel_id">子频道ID</param>
        /// <param name="role_id">身份组ID</param>
        /// <param name="add">添加的权限</param>
        /// <param name="remove">移除的权限</param>
        /// <returns></returns>
        public async Task<bool> UpdateRolePermissionAsync(string channel_id, string role_id, ChannelPermissionType add = ChannelPermissionType.None, ChannelPermissionType remove = ChannelPermissionType.None)
        {
            RawUpdateChannelRolePermissionApi rawUpdateChannelRolePermissionApi;

            var processedInfo = ApiFactory.Process(rawUpdateChannelRolePermissionApi, new Dictionary<ParamType, string>()
            {
                {ParamType.channel_id,channel_id },
                {ParamType.role_id,role_id }
            });

            var requestData = await _apiBase
                .WithJsonContentData(new Dictionary<string, object>()
                {
                    {"add",add is not ChannelPermissionType.None ? Convert.ToString((int)add,2):"" },
                    {"remove",remove is not ChannelPermissionType.None ?  Convert.ToString((int)remove,2):""}
                })
                .RequestAsync(processedInfo).ConfigureAwait(false);

            return requestData is null;
        }

        /// <summary>
        /// 修改指定子频道的权限
        /// </summary>
        /// <param name="channel_id">子频道ID</param>
        /// <param name="user_id">用户ID</param>
        /// <param name="add">添加的权限</param>
        /// <param name="remove">移除的权限</param>
        /// <returns></returns>
        public async Task<bool> UpdatePermissionsAsync(string channel_id, string user_id, ChannelPermissionType add = ChannelPermissionType.None, ChannelPermissionType remove = ChannelPermissionType.None)
        {
            RawUpdateChannelPermissionsApi rawUpdateChannelPermissionsApi;

            var processedInfo = ApiFactory.Process(rawUpdateChannelPermissionsApi, new Dictionary<ParamType, string>()
            {
                {ParamType.channel_id,channel_id },
                {ParamType.user_id,user_id }
            });

            var requestData = await _apiBase
                .WithJsonContentData(new Dictionary<string, object>()
                {
                    {"add",add is not ChannelPermissionType.None ? Convert.ToString((int)add,2):"" },
                    {"remove",remove is not ChannelPermissionType.None ?  Convert.ToString((int)remove,2):""}
                })
                .RequestAsync(processedInfo).ConfigureAwait(false);

            return requestData is null;
        }
    }
}