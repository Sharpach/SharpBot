﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TelegramBot.NyaBot.Args;
using TelegramBot.NyaBot.Replies;

namespace TelegramBot.NyaBot.Commands
{
    abstract class BaseCommand
    {
        public abstract bool ShouldInvoke(TelegramMessageEventArgs input);

        public abstract Task<IEnumerable<IReply>> Invoke(TelegramMessageEventArgs input);

        protected bool StringEquals(string x, string y)
        {

            return x!= null && y != null && string.Equals(x.Trim(), y.Trim(), StringComparison.OrdinalIgnoreCase);
        }

        protected bool StringOneOf(string x, IEnumerable<string> ys)
        {
            return ys.Any(y => StringEquals(x, y));
        }

        protected IEnumerable<IReply> Nothing => Enumerable.Empty<IReply>();



    }
}