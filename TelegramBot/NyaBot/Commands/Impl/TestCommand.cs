using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TelegramBot.NyaBot.Args;
using TelegramBot.NyaBot.Replies;
using TelegramBot.Util;

namespace TelegramBot.NyaBot.Commands
{
    class TestCommand : BaseCommand
    {
        public override bool ShouldInvoke(TelegramMessageEventArgs input)
        {
            return input.Message?.Text?.EndsWith("ест") ?? false;
        }

        public override Task<IEnumerable<IReply>> Invoke(TelegramMessageEventArgs input)
        {
            return Task.FromResult(GetReplies(input));
        }

        private IEnumerable<IReply> GetReplies(TelegramMessageEventArgs input)
        {
            yield return new TextReply(input.Message.Text.Replace("ест", "хуест"));
            yield return new ImageReply(File.ReadAllBytes(@"C:\Users\bashis\Desktop\photo_2017-02-08_14-48-48.jpg"));
        }
    }
}
