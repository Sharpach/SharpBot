using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Ninject;
using TelegramBot.Bot.Args;
using TelegramBot.Bot.Replies;
using TelegramBot.Logging;

namespace TelegramBot.Bot.Commands
{
    class CatCommand : BaseCommand
    {
        [Inject]
        public ILogger Logger { get; set; }

        public override bool ShouldInvoke(TelegramMessageEventArgs input)
        {
            return input?.Message?.Text?.ToUpper().Contains("КОТ") ?? false;
        }

        public override async Task<IEnumerable<IReply>> Invoke(TelegramMessageEventArgs input)
        {
            byte[] image;
            try
            {
                image = await GetRandomCatImage();
            }
            catch (WebException ex)
            {
                Logger.Log(ex);
                return Nothing;
            }
            
            await Task.Delay(500); //Limit Cat API requests per second

            return new IReply[]
            {
                new TextReply("Кто-то сказал " + input.Message.Text.Replace("кот", "КОТ") + "???"),
                new ImageReply(image)
            };
        }

        private static Task<byte[]> GetRandomCatImage()
        {
            using (var client = new WebClient())
            {
                return client.DownloadDataTaskAsync("http://thecatapi.com/api/images/get");
            }
        }
    }
}
