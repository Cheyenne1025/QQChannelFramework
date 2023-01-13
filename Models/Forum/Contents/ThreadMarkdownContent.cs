namespace MyBot.Models.Forum.Contents;

/// <summary>
/// 论坛帖子内容 - Markdown内容
/// </summary>
public class ThreadMarkdownContent : ThreadContent
{
    public override int Type => (int) Types.ThreadContentFormat.Markdown;

    public override string Content { get; set; }
}