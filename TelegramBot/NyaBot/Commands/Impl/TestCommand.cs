using System.Collections.Generic;
using System.Threading.Tasks;
using TelegramBot.NyaBot.Args;
using TelegramBot.Util;

namespace TelegramBot.NyaBot.Commands
{
    class TestCommand : BaseCommand
    {
        public override bool ShouldInvoke(TelegramMessageEventArgs input)
        {
            return input.Message?.Text?.EndsWith("ест") ?? false;
        }

        public override Task<IEnumerable<string>> Invoke(TelegramMessageEventArgs input)
        {
            return Task.FromResult(input.Message.Text.Replace("ест", "хуест").Yield());
        }
    }
}
