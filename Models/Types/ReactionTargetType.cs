namespace MyBot.Models.Types
{
	/// <summary>
	/// 表态对象类型
	/// </summary>
	public enum ReactionTargetType : int
	{
		/// <summary>
        /// 消息
        /// </summary>
		Message,
		/// <summary>
        /// 帖子
        /// </summary>
		Posts,
		/// <summary>
        /// 评论
        /// </summary>
		Comments,
		/// <summary>
        /// 回复
        /// </summary>
		Reply
	}
}

