using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.NyaBot.Args;
using TelegramBot.NyaBot.Replies;

namespace TelegramBot.NyaBot.Commands.Impl
{
    class CatCommand : BaseCommand
    {
        public override bool ShouldInvoke(TelegramMessageEventArgs input)
        {
            throw new NotImplementedException();
        }

        public override Task<IEnumerable<IReply>> Invoke(TelegramMessageEventArgs input)
        {
            throw new NotImplementedException();
        }
    }
}
