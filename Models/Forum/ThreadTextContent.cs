namespace QQChannelFramework.Models.Forum;

/// <summary>
/// 论坛帖子内容 - 文字内容
/// </summary>
public class ThreadTextContent
{
    public string Type => Types.ThreadContentFormat.Text.ToString();

    public string Content { get; set; }
}
