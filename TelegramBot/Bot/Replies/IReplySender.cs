using System.Threading.Tasks;

namespace TelegramBot.NyaBot.Replies
{
    public interface IReplySender
    {
        Task Send(IReply reply, long chatId);
    }
}