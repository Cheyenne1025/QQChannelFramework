namespace QQChannelFramework.Models.Forum;

/// <summary>
/// 论坛帖子内容 - Markdown内容
/// </summary>
public class ThreadMarkdownContent
{
    public string Type => Types.ThreadContentFormat.Markdown.ToString();
    
    public string Content { get; set; }
}
