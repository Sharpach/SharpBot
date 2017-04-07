using System.Collections.Generic;
using System.Threading.Tasks;
using TelegramBot.NyaBot.Args;

namespace TelegramBot.NyaBot.Commands
{
    public interface ICommandInvoker
    {
        Task<IEnumerable<string>> Invoke(TelegramMessageEventArgs input);
    }
}