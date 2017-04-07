using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Ninject;
using TelegramBot.API;
using TelegramBot.API.Models;
using TelegramBot.Logging;
using TelegramBot.NyaBot.Args;
using TelegramBot.NyaBot.Commands;
using TelegramBot.NyaBot.Replies;
using TelegramBot.NyaBot.Types;
using TelegramBot.NyaBot.Updates;

namespace TelegramBot.NyaBot
{
    public class NyanBot
    {
        private readonly ApiClient _api;
        private readonly ICommandInvoker _invoker;
        private readonly IUpdatesProvider _updatesProvider;
        private readonly IReplySender _replySender;
       
        [Inject]
        public ILogger Logger { get; set; }

        public NyanBot(ApiClient api, ICommandInvoker invoker, IUpdatesProvider updatesProvider, IReplySender replySender)
        {
            _api = api;
            _invoker = invoker;
            _updatesProvider = updatesProvider;
            _replySender = replySender;
        }

        internal async Task Start()
        {
            IsRunning = true;
            await UpdatesThread();
        }

        internal void Stop()
        {
            IsRunning = false;
        }

        public bool IsRunning { get; private set; }

        private async Task UpdatesThread()
        {
            Logger?.Log(LogLevel.Message, "Бот запущен.");
            while (IsRunning)
            {
                var updates = await GetUpdates();

                foreach (var update in updates)
				{
                    if (update.Message != null)
                    {
                        await ProcessMessage(update.Message);
                    }
                }

                await Task.Delay(1000);
            }
        }

        private async Task ProcessMessage(Message message)
        {
            var args = new TelegramMessageEventArgs
            {
                ChatId = message.Chat.Id,
                MessageId = message.MessageId,
                From = message.From,
                Message = message
            };

            var results = await _invoker.Invoke(args);
            foreach (var result in results)
            {
                await _replySender.Send(result, args.ChatId);
                await Task.Delay(300);
            }
        }

        private Task<ICollection<Update>> GetUpdates()
        {
            try
            {
                return _updatesProvider.GetUpdates();
            }
            catch (Exception ex)
            {
                IsRunning = false;
                if (ex is WebException || ex is JsonException)
                {
                    Logger?.Log(LogLevel.Fatal, ex.Message);
                    return Task.FromResult((ICollection<Update>)new Update[]{});
                }
                throw;
            }
        }
	}
}