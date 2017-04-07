using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Ninject;
using TelegramBot.API;
using TelegramBot.Bot.Args;
using TelegramBot.Bot.Replies;

namespace TelegramBot.Bot.Commands
{
    class CommandInvoker : ICommandInvoker
    {
        private readonly ApiClient _client;

        public CommandInvoker(ApiClient client, IKernel kernel)
        {
            _client = client;
            Me = new MeCommand(_client);
            Commands = GetCommands(kernel).ToList();
        }

        public ICollection<BaseCommand> Commands { get; } 

        public readonly MeCommand Me;

        public async Task<IEnumerable<IReply>> Invoke(TelegramMessageEventArgs input)
        {
            var result = new List<IReply>();
            foreach (var command in Commands)
            {
                if (!command.ShouldInvoke(input)) continue;
                var output = await command.Invoke(input);
                result.AddRange(output);
            }
            return result;
        }

        private IEnumerable<BaseCommand> GetCommands(IKernel kernel)
        {
            var types = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => typeof(BaseCommand).IsAssignableFrom(t) && !t.IsAbstract);
            foreach (var commandType in types)
            {
                yield return (BaseCommand)kernel.Get(commandType);
            }
        }
    }
}
