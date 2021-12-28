using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using QQChannelFramework.Api.Base;
using QQChannelFramework.Api.Raws;
using QQChannelFramework.Api.Types;
using QQChannelFramework.Models;

namespace QQChannelFramework.Api
{
    sealed partial class QQChannelApi
    {
        private static ChildChannelPermissionsApi _childChannelPermissionsApi;

        public ChildChannelPermissionsApi GetChildChannelPermissionsApi()
        {
            if (_childChannelPermissionsApi is null)
            {
                _childChannelPermissionsApi = new(apiBase);
            }

            return _childChannelPermissionsApi;
        }
    }

    /// <summary>
    /// 子频道权限Api
    /// </summary>
    public class ChildChannelPermissionsApi
    {
        readonly ApiBase _apiBase;

        public ChildChannelPermissionsApi(ApiBase apiBase)
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
            RawGetChildChannelPermissionsApi rawGetChildChannelPermissionsApi;

            var processedInfo = ApiFactory.Process(rawGetChildChannelPermissionsApi, new Dictionary<ParamType, string>()
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
        /// <param name="channel_id">子频道ID</param>
        /// <param name="role_id">身份组ID</param>
        /// <returns></returns>
        public async Task<ChannelRolePermissions> GetChildChannelRolePermission(string channel_id, string role_id)
        {
            RawGetChildChannelRolePermissionApi rawGetChildChannelRolePermissionApi;

            var processedInfo = ApiFactory.Process(rawGetChildChannelRolePermissionApi, new Dictionary<ParamType, string>()
            {
                {ParamType.channel_id,channel_id},
                {ParamType.role_id,role_id}
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
            RawUpdateChildChannelRolePermissionApi rawUpdateChildChannelRolePermissionApi;

            var processedInfo = ApiFactory.Process(rawUpdateChildChannelRolePermissionApi, new Dictionary<ParamType, string>()
            {
                {ParamType.channel_id,channel_id },
                {ParamType.role_id,role_id }
            });

            var requestData = await _apiBase
                .WithData(new Dictionary<string, object>()
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
            RawUpdateChildChannelPermissionsApi rawUpdateChildChannelPermissionsApi;

            var processedInfo = ApiFactory.Process(rawUpdateChildChannelPermissionsApi, new Dictionary<ParamType, string>()
            {
                {ParamType.channel_id,channel_id },
                {ParamType.user_id,user_id }
            });

            var requestData = await _apiBase
                .WithData(new Dictionary<string, object>()
                {
                    {"add",add is not ChannelPermissionType.None ? Convert.ToString((int)add,2):"" },
                    {"remove",remove is not ChannelPermissionType.None ?  Convert.ToString((int)remove,2):""}
                })
                .RequestAsync(processedInfo).ConfigureAwait(false);

            return requestData is null;
        }
    }
}