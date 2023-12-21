using MyBot.WS;
namespace MyBot.Expansions.Bot;

sealed partial class ChannelBot
{
    /// <summary>
    /// <para>触发时机: </para>
    /// <para>指令触发时</para>
    /// </summary>
    public event CommandTriggerDelegate CommandTrigger;
}