namespace MyBot.Models.Types
{
    /// <summary>
    /// 元素类型
    /// </summary>
    public enum ElemType : int
    {
        /// <summary>
        /// 文本
        /// </summary>
        ELEM_TYPE_TEXT = 1,
        /// <summary>
        /// 图片
        /// </summary>
        ELEM_TYPE_IMAGE = 2,
        /// <summary>
        /// 视频
        /// </summary>
        ELEM_TYPE_VIDEO = 3,
        /// <summary>
        /// URL
        /// </summary>
        ELEM_TYPE_URL = 4
    }
}
