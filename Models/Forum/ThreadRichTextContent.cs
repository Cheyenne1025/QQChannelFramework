using Newtonsoft.Json.Linq;

namespace QQChannelFramework.Models.Forum;

/// <summary>
/// 论坛帖子内容 - 富文本内容
/// </summary>
public class ThreadRichTextContent
{
    public string Type => Types.ThreadContentFormat.Json.ToString();
    
    public JObject Content { get; set; }
}
