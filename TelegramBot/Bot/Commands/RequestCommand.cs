using System.Threading.Tasks;
using TelegramBot.API;

namespace TelegramBot.Bot.Commands
{
    abstract class RequestCommand<T> : BaseCommand
    {
        private readonly ApiClient _client;
        public abstract string Method { get; }

        public RequestCommand(ApiClient client)
        {
            _client = client;
        }

        protected Task<T> Get(object obj = null)
        {
            return _client.SendRequestAsync<T>(Method);
        }
    }
}
