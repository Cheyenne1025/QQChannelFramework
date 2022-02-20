using QQChannelFramework.Api.Base;
using QQChannelFramework.Api.Types;

namespace QQChannelFramework.Api;

/// <summary>
/// QQ频道机器人OpenApi
/// </summary>
public sealed partial class QQChannelApi { 
    
    private ApiBase apiBase {
        get {
            var ret = new ApiBase(OpenApiAccessInfo);
            switch (Identity) {
                case Identity.Bot: ret.UseBotIdentity(); break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            switch (RequestMode) {
                case RequestMode.Release: ret.UseReleaseMode(); break;
                case RequestMode.SandBox: ret.UseSandBoxMode(); break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return ret;
        }
    }
    
    public OpenApiAccessInfo OpenApiAccessInfo { get; private set; }
    
    public QQChannelApi(OpenApiAccessInfo openApiAccessInfo) {
        OpenApiAccessInfo = openApiAccessInfo;
    }

    /// <summary>
    /// 正式/沙箱模式
    /// </summary>
    public RequestMode RequestMode { get; set; }
    public Identity Identity { get; set; }

    /// <summary>
    /// 使用正式模式 (默认)
    /// </summary>
    /// <returns></returns>
    public QQChannelApi UseReleaseMode() {
        RequestMode = RequestMode.Release;
        return this;
    }

    /// <summary>
    /// 使用沙盒模式
    /// </summary>
    /// <returns></returns>
    public QQChannelApi UseSandBoxMode() {
        RequestMode = RequestMode.SandBox;
        return this;
    }

    /// <summary>
    /// 使用Bot身份
    /// </summary>
    /// <returns></returns>
    public QQChannelApi UseBotIdentity() {
        Identity = Identity.Bot;
        return this;
    }
}