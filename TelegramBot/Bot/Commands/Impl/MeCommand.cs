using System.Collections.Generic;
using System.Threading.Tasks;
using TelegramBot.API;
using TelegramBot.API.Models;
using TelegramBot.Bot.Args;
using TelegramBot.Bot.Replies;
using TelegramBot.Util;

namespace TelegramBot.Bot.Commands
{
    class MeCommand : RequestCommand<User>
    {
        public override string Method => "getme";

        public override bool ShouldInvoke(TelegramMessageEventArgs input)
        {
            return StringEquals(input.Message.Text, "getme");
        }

        public override async Task<IEnumerable<IReply>> Invoke(TelegramMessageEventArgs input)
        {
            var user = await Get();
            return new TextReply($"Привет, меня зовут {user.Username}").Yield();
        }


        public MeCommand(ApiClient client) : base(client)
        {
        }
    }
}
