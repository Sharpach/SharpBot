using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TelegramBot.NyaBot.Args;

namespace TelegramBot.NyaBot.Commands
{
    abstract class BaseCommand
    {
        public abstract bool ShouldInvoke(TelegramMessageEventArgs input);

        public abstract Task<IEnumerable<string>> Invoke(TelegramMessageEventArgs input);

        protected void SafeInvoke(Action action)
        {
            try
            {
                action();
            }
            catch (JsonException e)
            {
                Logger.LogError(e);
                throw;
            }
        }

        protected bool StringEquals(string x, string y)
        {

            return x!= null && y != null && string.Equals(x.Trim(), y.Trim(), StringComparison.OrdinalIgnoreCase);
        }

        protected bool StringOneOf(string x, IEnumerable<string> ys)
        {
            return ys.Any(y => StringEquals(x, y));
        }

        protected IEnumerable<string> Nothing => Enumerable.Empty<string>();



    }
}
