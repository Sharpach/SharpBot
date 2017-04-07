using System;
using TelegramBot.API.Models;

namespace TelegramBot.NyaBot.Args
{
    internal class CallbackQueryEventArgs : EventArgs
    {
        public CallbackQuery CallbackQuery { get; set; }
    }
}
