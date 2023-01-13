namespace MyBot.Models.Types
{
    /// <summary>
    /// 日程提醒类型
    /// </summary>
    public enum RemindType : int
    {
        /// <summary>
        /// 不提醒
        /// </summary>
        NoReminders,
        /// <summary>
        /// 开始时提醒
        /// </summary>
        ReminderATheBeginning,
        /// <summary>
        /// 开始前5分钟提醒
        /// </summary>
        Reminder5MinutesBeforeStart,
        /// <summary>
        /// 开始前15分钟提醒
        /// </summary>
        Reminder15MinutesBeforeStart,
        /// <summary>
        /// 开始前30分钟提醒
        /// </summary>
        Reminder30MinutesBeforeStart,
        /// <summary>
        /// 开始前60分钟提醒
        /// </summary>
        Reminder60MinutesBeforeStart,
    }
}

