namespace MyBot.Models.Forum.Contents;

/// <summary>
/// 论坛帖子内容 - 文字内容
/// </summary>
public class ThreadTextContent : ThreadContent
{
    public override int Type => (int) Types.ThreadContentFormat.Text;

    public override string Content { get; set; }
}
