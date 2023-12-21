using MyBot.Models.Types;
namespace MyBot.Models.Forum
{
    /// <summary>
    /// 富文本 - 段落属性
    /// </summary>
    public class ParagraphProps
    {
        /// <summary>
        /// 段落对齐方向属性
        /// </summary>
        [Newtonsoft.Json.JsonProperty("alignment")]
        public Alignment Alignment { get; set; }
    }
}
