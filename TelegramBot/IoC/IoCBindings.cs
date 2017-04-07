using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;
using TelegramBot.NyaBot;

namespace TelegramBot.IoC
{
    class IoCBindings : NinjectModule
    {
        public override void Load()
        {
            Bind<NyanBot>().ToSelf();
            Bind<BotApiClient>().ToConstant(new BotApiClient(ConfigurationManager.AppSettings["token"]));

        }
    }
}
