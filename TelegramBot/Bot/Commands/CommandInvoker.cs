﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.API;
using TelegramBot.NyaBot.Args;
using TelegramBot.NyaBot.Replies;

namespace TelegramBot.NyaBot.Commands
{
    class CommandInvoker : ICommandInvoker
    {
        private readonly ApiClient _client;

        public CommandInvoker(ApiClient client)
        {
            _client = client;
            Me = new MeCommand(_client);
            Commands = new BaseCommand[] {Me, new TestCommand(), new CatCommand(), };
        }

        public IReadOnlyList<BaseCommand> Commands { get; } 

        public readonly MeCommand Me;

        public async Task<IEnumerable<IReply>> Invoke(TelegramMessageEventArgs input)
        {
            var result = new List<IReply>();
            foreach (var command in Commands)
            {
                if (!command.ShouldInvoke(input)) continue;
                var output = await command.Invoke(input);
                foreach (var item in output)
                {
                    result.Add(item);
                }
            }
            return result;
        }
    }
}