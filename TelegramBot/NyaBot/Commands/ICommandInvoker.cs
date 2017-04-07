using System.Collections.Generic;
using System.Threading.Tasks;
using TelegramBot.NyaBot.Args;
using TelegramBot.NyaBot.Replies;

namespace TelegramBot.NyaBot.Commands
{
    public interface ICommandInvoker
    {
        Task<IEnumerable<IReply>> Invoke(TelegramMessageEventArgs input);
    }
}