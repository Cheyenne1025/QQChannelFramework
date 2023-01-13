using System.Threading.Tasks;
using MyBot.Api;
using MyBot.Exceptions;
using MyBot.Expansions.Bot;
using MyBot.Models.MessageModels;
using MyBot.Models.Returns;

namespace MyBot.Tools;
public static class MessageAuditExtension
{
    /// <summary>
    /// 如果发出的消息需要审核，则等待至审核通过。需要RegisterAuditEvent
    /// </summary>
    /// <param name="task"></param>
    /// <param name="bot"></param>
    /// <param name="api"></param>
    /// <returns>审核通过后的消息</returns>
    public static async Task<Message> Audit(this Task<Message> task, ChannelBot bot, MessageApi api)
    {
        try
        {
            return await task.ConfigureAwait(false);
        }
        catch (MessageAuditException e)
        {
            var tcs = new TaskCompletionSource();
            WS.FunctionWebSocket.AuditDelegate eh = null!;
            Message message = default!;
            eh = async (MessageAudited audit) =>
            {
                if (audit.AuditId != e.AuditId) return;
                bot.MessageAuditPass -= eh;
                message = await api.GetMessageAsync(audit.ChannelId, audit.MessageId);
                tcs.SetResult();
            };
            bot.MessageAuditPass += eh;
            await tcs.Task;
            return message;
        }
        catch
        {
            throw;
        }
    }
}
