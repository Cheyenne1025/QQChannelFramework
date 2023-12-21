namespace MyBot.Models.Forum.Contents;

/// <summary>
/// 论坛帖子内容 - Html内容
/// </summary>
public class ThreadHtmlContent : ThreadContent
{
    public override int Type => (int)Types.ThreadContentFormat.Html;

    public override string Content { get; set; }
}
