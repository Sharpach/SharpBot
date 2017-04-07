using System;
using TelegramBot.API_Classes;

namespace TelegramBot.NyaBot.Args
{
    internal class CallbackQueryEventArgs : EventArgs
    {
        public CallbackQuery CallbackQuery { get; set; }
    }
}
