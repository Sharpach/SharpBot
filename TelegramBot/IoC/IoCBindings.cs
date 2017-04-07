using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;
using TelegramBot.API;
using TelegramBot.NyaBot;
using TelegramBot.NyaBot.Commands;
using TelegramBot.NyaBot.Updates;

namespace TelegramBot.IoC
{
    class IoCBindings : NinjectModule
    {
        public override void Load()
        {
            Bind<NyanBot>().ToSelf();
            Bind<ApiClient>().ToConstant(new ApiClient(ConfigurationManager.AppSettings["token"]));
            Bind<ICommandInvoker>().To<CommandInvoker>();
            Bind<IUpdatesProvider>().To<UpdatesProvider>();
        }
    }
}
