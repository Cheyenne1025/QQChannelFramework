namespace MyBot.Models
{
    /// <summary>
    /// 指令状态
    /// </summary>
    public class CommandState
    {
        /// <summary>
        /// 当前步骤
        /// </summary>
        public int Step { get; set; }

        /// <summary>
        /// 最大步骤
        /// </summary>
        public int MaxStep { get; set; }

        /// <summary>
        /// 附带数据
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// 下一步
        /// </summary>
        public void Next()
        {
            Step += 1;
        }

        /// <summary>
        /// 上一步
        /// </summary>
        public void Prev()
        {
            if(Step != 0)
            {
                Step -= 1;
            }
        }

        public void Reset() => Step = 0;
    }
}

