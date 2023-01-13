namespace MyBot.Models.ParamModels;

/// <summary>
/// 创建频道身份组参数模型 - 标识需要设置/修改哪些字段
/// </summary>
public struct Filter
{
    /// <summary>
    /// 是否设置名称
    /// </summary>
    public bool Name { get; set; }

    /// <summary>
    /// 是否设置颜色
    /// </summary>
    public bool Color { get; set; }

    /// <summary>
    /// 是否设置在成员列表中单独展示
    /// </summary>
    public bool Hoist { get; set; }
}