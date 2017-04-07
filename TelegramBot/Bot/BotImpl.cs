using System;
using System.Threading.Tasks;
using Ninject;
using TelegramBot.API;
using TelegramBot.API.Models;
using TelegramBot.Bot.Args;
using TelegramBot.Bot.Commands;
using TelegramBot.Bot.Replies;
using TelegramBot.Bot.Updates;
using TelegramBot.Logging;

namespace TelegramBot.Bot
{
    public class BotImpl
    {
        private readonly ApiClient _api;
        private readonly ICommandInvoker _invoker;
        private readonly IUpdatesProvider _updatesProvider;
        private readonly IReplySender _replySender;
       
        [Inject]
        public ILogger Logger { get; set; }

        public BotImpl(ApiClient api, ICommandInvoker invoker, IUpdatesProvider updatesProvider, IReplySender replySender)
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
                await Task.Delay(1000);

                try
                {
                    var updates = await _updatesProvider.GetUpdates();

                    foreach (var update in updates)
                    {
                        if (update.Message != null)
                        {
                            await ProcessMessage(update.Message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ProcessException(ex);
                }
              
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
        
        private void ProcessException(Exception ex)
        {
            IsRunning = false;
            Logger?.Log(LogLevel.Fatal, ex.Message);
        }

    }
}