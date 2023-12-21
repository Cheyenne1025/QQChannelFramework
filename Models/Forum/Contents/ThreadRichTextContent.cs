namespace MyBot.Models.Forum.Contents;

/// <summary>
/// 论坛帖子内容 - 富文本内容
/// </summary>
public class ThreadRichTextContent : ThreadContent
{
    public override int Type => (int)Types.ThreadContentFormat.Json;
    
    public override string Content { get; set; }
}
