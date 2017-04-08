﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TelegramBot.API;
using TelegramBot.Bot.Args;
using TelegramBot.Bot.Replies;
using TelegramBot.Bot.Types;

namespace TelegramBot.Bot.Commands
{
    class SendMessageCommand : BaseCommand
    {
        private readonly ApiClient _client;
        private readonly MessageToSend _message;

        public SendMessageCommand(ApiClient client, MessageToSend message)
        {
            _client = client;
            _message = message;
        }

        public override bool ShouldInvoke(TelegramMessageEventArgs input)
        {
            return false;
        }

        public override async Task<IEnumerable<IReply>> Invoke(TelegramMessageEventArgs input)
        {
            await _client.SendRequestAsync<object>("sendMessage", _message);
            return Nothing;
        }
    }
}
