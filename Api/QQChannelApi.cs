using System;
using QQChannelFramework.Api.Base;

namespace QQChannelFramework.Api
{
    public sealed partial class QQChannelApi
    {
        private static ApiBase apiBase;

        public QQChannelApi(OpenApiAccessInfo openApiAccessInfo)
        {
            apiBase = new ApiBase(openApiAccessInfo);
        }

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
        //[Obsolete("官方暂未开放使用", true)]
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
}