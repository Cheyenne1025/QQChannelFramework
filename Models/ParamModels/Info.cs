using System;
namespace QQChannelFramework.Models.ParamModels
{
    /// <summary>
    /// 创建频道身份组参数模型 - 携带需要设置/修改的字段内容
    /// </summary>
    public struct Info
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 不带A通道的RGB十六进制颜色值 例如: #ff6542
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// 在成员列表中单独展示
        /// </summary>
        public bool Hoist { get; set; }
    }
}