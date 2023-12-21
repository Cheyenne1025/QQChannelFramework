using MyBot.Api.Types;
namespace MyBot.Api.Raws;

/// <summary>
/// 源Api信息 - 获取指定子频道身份组的权限
/// </summary>
public struct RawGetChannelRolePermissionApi : Base.IRawApiInfo
{
    public string Version => "1.0";

    
    public string Url => "/channels/{channel_id}/roles/{role_id}/permissions";

    public MethodType Method => MethodType.GET;
}