using QQChannelFramework.Api.Base;
using QQChannelFramework.Api.Types;

namespace QQChannelFramework.Api;

/// <summary>
/// QQ频道机器人OpenApi
/// </summary>
public sealed partial class QQChannelApi
{
    private static ApiBase apiBase;

    public QQChannelApi(OpenApiAccessInfo openApiAccessInfo)
    {
        if (apiBase is null) {
            apiBase = new ApiBase(openApiAccessInfo);
        } 
    }

    /// <summary>
    /// 正式/沙箱模式
    /// </summary>
    public RequestMode RequestMode => apiBase._requestMode;
    
    /// <summary>
    /// 使用正式模式 (默认)
    /// </summary>
    /// <returns></returns>
    public QQChannelApi UseReleaseMode()
    {
        apiBase.UseReleaseMode();

        return this;
    }

    /// <summary>
    /// 使用沙盒模式
    /// </summary>
    /// <returns></returns>
    public QQChannelApi UseSandBoxMode()
    {
        apiBase.UseSandBoxMode();

        return this;
    }

    /// <summary>
    /// 使用Bot身份
    /// </summary>
    /// <returns></returns>
    public QQChannelApi UseBotIdentity()
    {
        apiBase.UseBotIdentity();

        return this;
    }
}