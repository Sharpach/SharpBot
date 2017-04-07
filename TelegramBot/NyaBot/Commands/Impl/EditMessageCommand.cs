using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TelegramBot.API;
using TelegramBot.NyaBot.Args;
using TelegramBot.NyaBot.Types;

namespace TelegramBot.NyaBot.Commands
{
    class EditMessageCommand : BaseCommand
    {
        private readonly ApiClient _client;
        private readonly EditMessageData _data;

        public EditMessageCommand(ApiClient client, EditMessageData data)
        {
            _client = client;
            _data = data;
        }

        public override bool ShouldInvoke(TelegramMessageEventArgs input)
        {
            throw new NotImplementedException();
        }

        public override async Task<IEnumerable<string>> Invoke(TelegramMessageEventArgs input)
        {
            await _client.SendRequestAsync<object>("editMessageText", _data);
            return Nothing;
        }
    }
}
