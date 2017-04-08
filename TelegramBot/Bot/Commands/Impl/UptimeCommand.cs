using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.Bot.Args;
using TelegramBot.Bot.Replies;

namespace TelegramBot.Bot.Commands.Impl
{
    class UptimeCommand : Command
    {
        private DateTime StartDateTime { get; }
        public UptimeCommand()
        {
            StartDateTime = DateTime.Now;
        }

        public override bool ShouldInvoke(TelegramMessageEventArgs input)
        {
            return MessageEquals(input, "аптайм", "uptime", "/uptime");
        }

        protected override Task<IEnumerable<IReply>> OnInvoke(TelegramMessageEventArgs input)
        {
            TimeSpan diff = DateTime.Now - StartDateTime;
            return FromResult(new TextReply(diff.ToString(@"hh\:mm\:ss")));
        }
    }
}
