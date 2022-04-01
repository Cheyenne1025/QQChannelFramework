namespace QQChannelFramework.Models.Forum;

/// <summary>
/// 论坛帖子内容 - Html内容
/// </summary>
public class ThreadHtmlContent
{
    public string Type => Types.ThreadContentFormat.Html.ToString();

    public string Content { get; set; }
}
