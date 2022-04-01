namespace QQChannelFramework.Models.Forum;

/// <summary>
/// 抽象帖子内容
/// </summary>
public abstract class ThreadContent
{
    public abstract int Type { get; }

    public abstract string Content { get; set; }
}