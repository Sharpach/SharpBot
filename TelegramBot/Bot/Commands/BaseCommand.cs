using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TelegramBot.Bot.Args;
using TelegramBot.Bot.Replies;
using TelegramBot.Util;

namespace TelegramBot.Bot.Commands
{
    abstract class BaseCommand
    {
        public abstract bool ShouldInvoke(TelegramMessageEventArgs input);

        public abstract Task<IEnumerable<IReply>> Invoke(TelegramMessageEventArgs input);

        protected bool StringEquals(string x, string y)
        {
            return x!= null && y != null && string.Equals(x.Trim(), y.Trim(), StringComparison.OrdinalIgnoreCase);
        }

        protected bool MessageEquals(TelegramMessageEventArgs args, params string[] values)
        {
            if (args?.Message?.Text == null) return false;
            return StringOneOf(args?.Message?.Text, values);
        }

        protected bool StringOneOf(string x, IEnumerable<string> ys)
        {
            return ys.Any(y => StringEquals(x, y));
        }

        protected IEnumerable<IReply> Nothing => Enumerable.Empty<IReply>();

        protected static Task<IEnumerable<IReply>> FromResult(IReply reply)
        {
            return Task.FromResult(reply.Yield());
        }


    }
}
