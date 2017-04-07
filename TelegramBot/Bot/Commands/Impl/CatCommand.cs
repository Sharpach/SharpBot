using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using TelegramBot.NyaBot.Args;
using TelegramBot.NyaBot.Replies;

namespace TelegramBot.NyaBot.Commands
{
    class CatCommand : BaseCommand
    {
        public override bool ShouldInvoke(TelegramMessageEventArgs input)
        {
            return input?.Message?.Text?.ToUpper().Contains("КОТ") ?? false;
        }

        public override async Task<IEnumerable<IReply>> Invoke(TelegramMessageEventArgs input)
        {
            byte[] image = await GetRandomCatImage();
            await Task.Delay(500); //Limit Cat API requests per second

            return new IReply[]
            {
                new TextReply("Кто-то сказал " + input.Message.Text.Replace("кот", "КОТ") + "???"),
                new ImageReply(image)
            };
        }

        private Task<byte[]> GetRandomCatImage()
        {
            using (var client = new WebClient())
            {
                return client.DownloadDataTaskAsync("http://thecatapi.com/api/images/get");
            }
        }
    }
}
