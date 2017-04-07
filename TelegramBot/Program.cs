using System;
using System.Threading.Tasks;
using Ninject;
using TelegramBot.API.Models;
using TelegramBot.IoC;
using TelegramBot.NyaBot;
using TelegramBot.NyaBot.Args;

namespace TelegramBot
{
    class Program
    {
        static NyanBot _bot = null;
        static BotHelper _botHelper = null;
        static Random _random = null;
        static DateTime _startTime;

        private static readonly IKernel _kernel = CreateKernel();

        private static IKernel CreateKernel()
        {
            return new StandardKernel(new IoCBindings());
        }

        static void Main(string[] args)
        {
            _bot = _kernel.Get<NyanBot>();
            _botHelper = new BotHelper("BaaakaBot");

            _random = new Random();

            _startTime = DateTime.Now;
            Task.WaitAll(_bot.Start());
        }

        //static async void Bot_OnMessage(TelegramMessageEventArgs a)
        //{
        //    // проверка: текстовое ли сообщеине. В дальнейшем нужно определять его тип ещё 
        //    // во время парсинга в классе бота
        //    if (a.Message.Text == null)
        //    {
        //        return;
        //    }

        //    string text = a.Message.Text;
        //    Logger.LogMessage($"{a.From.Username ?? a.From.Id.ToString()}: {text}");

        //    // демонстрация команды с аргументами
        //    if (_botHelper.CheckCommand(text, "/roll", "ролл", "roll") && _botHelper.CheckTime(a.From.Id))
        //    {
        //        int maxValue = 100;
        //        var msgArgs = _botHelper.GetCommandArgs(text);

        //        if (msgArgs.Length > 0)
        //        {
        //            int o;
        //            if (Int32.TryParse(msgArgs[0], out o))
        //            {
        //                maxValue = Math.Abs(o);
        //            }
        //        }

        //        string result = _random.Next(++maxValue).ToString();

        //        _bot.SendMessage(a.ChatId, result, replyToMessageId: a.MessageId);
        //    }

        //    // демонстрация отправки изображения
        //    if (_botHelper.CheckCommand(text, "картинка") && _botHelper.CheckTime(a.From.Id))
        //    {
        //        _bot.SendPhoto(a.ChatId, @"https://telegram.org/img/t_logo.png", replyToMessageId: a.MessageId);
        //    }

        //    // демонстрация отправки стикера
        //    if (_botHelper.CheckCommand(text, "o_o", "o.o", "о_о", "о.о") && _botHelper.CheckTime(a.From.Id))
        //    {
        //        // магическая строка - это id стикера
        //        _bot.SendSticker(a.ChatId, "CAADBAADxgIAAlI5kwbR0EZ_zGfzwQI", replyToMessageId: a.MessageId);
        //    }

        //    if (_botHelper.CheckCommand(text, "аптайм", "/uptime", "uptime") && _botHelper.CheckTime(a.From.Id))
        //    {
        //        var uptime = DateTime.Now - _startTime;

        //        _bot.SendMessage(a.ChatId, uptime.ToString(@"hh\:mm\:ss"));
        //    }

        //    // демонстрация получения списка аргументов
        //    if (_botHelper.CheckCommand(text, "аргументы") && _botHelper.CheckTime(a.From.Id))
        //    {
        //        var msgArgs = _botHelper.GetCommandArgs(text);

        //        string result = String.Join(";", msgArgs);

        //        _bot.SendMessage(a.ChatId, "{" + result + "}");
        //    }

        //    // калькулятор
        //    if (_botHelper.CheckCommand(text, "!") && _botHelper.CheckTime(a.From.Id, 3))
        //    {
        //        using (var table = new System.Data.DataTable())
        //        {
        //            try
        //            {
        //                string result = table.Compute(String.Join(" ", _botHelper.GetCommandArgs(text)), String.Empty).ToString();
        //                _bot.SendMessage(a.ChatId, result, replyToMessageId: a.MessageId);
        //            }
        //            catch
        //            {
        //                _bot.SendMessage(a.ChatId, "Ошибка!", replyToMessageId: a.MessageId);
        //            }
        //        }
        //    }

        //    // демонстрация клавиатуры
        //    if (_botHelper.CheckCommand(text, "клава"))
        //    {
        //        var kb = new ReplyKeyboardMarkup
        //        {
        //            // клавиатура выглядит как массив строк из кнопок, внутри которых массив кнопок: keyboard[строки], строка[кнопки],
        //            // но если это сложно, то можно просто использовать билдер клавиатуры из вспомогательного класса
        //            Keyboard = BotHelper.BuildKeyboard(BotHelper.BuildButtonsRow("ролл", "рулетка"),
        //                                               BotHelper.BuildButtonsRow("аптайм", "сосач"),
        //                                               BotHelper.BuildButtonsRow("скрыть"))
        //        };

        //        _bot.SendMessage(a.ChatId, "Выбирай!", replyMarkup: kb);
        //    }

        //    // демонстрация инлайн кнопок
        //    if (_botHelper.CheckCommand(text, "инлайн"))
        //    {
        //        var kb = new InlineKeyboardMarkup
        //        {
        //            InlineKeyboard = new[]
        //            {
        //                // ряд 1
        //                new []
        //                {
        //                    new InlineKeyboardButton
        //                    {
        //                        Text = "0",
        //                        CallbackData = "0"
        //                    }
        //                }
        //            }
        //        };

        //        _bot.SendMessage(a.ChatId, "мяумур", replyMarkup: kb);
        //    }

        //    // демонстрация скрытия клавиатуры
        //    if (_botHelper.CheckCommand(text, "скрыть"))
        //    {
        //        var kb = new ReplyKeyboardRemove
        //        {
        //            RemoveKeyboard = true
        //        };
        //        _bot.SendMessage(a.ChatId, "ок", replyMarkup: kb);
        //    }

        //    if (_botHelper.CheckCommand(text, "сосач", "2ch", @"/2ch", "двач") && _botHelper.CheckTime(a.From.Id))
        //    {
        //        var arg = "b";
        //        var ar = _botHelper.GetCommandArgs(text);
        //        if (ar.Length > 0)
        //        {
        //            if (Sosach.CheckBoardName(ar[0]))
        //            {
        //                arg = ar[0];
        //            }
        //        }

        //        var sosach = new Sosach();
        //        var list = sosach.GetThreadsList(arg);
        //        _bot.SendMessage(a.ChatId, (list.Length > 0) ? list : "Ошибка!");
        //    }

        //    // демонстрация отправки действия бота
        //    if (_botHelper.CheckCommand(text, "действие") && _botHelper.CheckTime(a.From.Id, 100))
        //    {
        //        _bot.SendChatAction(a.ChatId, NyaBot.Types.ChatAction.UploadPhoto);
        //    }

        //    // для даунов
        //    if (_botHelper.CheckCommand(text, "ь") && _botHelper.CheckTime(a.From.Id))
        //    {
        //        _bot.SendMessage(a.ChatId, "http://tsya.ru");
        //    }

        //    // демо асинхронности
        //    if (_botHelper.CheckCommand(text, "асинк", "async") && _botHelper.CheckTime(a.From.Id))
        //    {
        //        await _bot.SendMessageAsync(a.ChatId, "Это сообщение отправлено асинхронно!", replyToMessageId: a.MessageId);
        //    }

        //    if (_botHelper.CheckCommand(text, "getme") && _botHelper.CheckTime(a.From.Id))
        //    {
        //        var me = _bot.GetMe();
        //        if (me == null)
        //        {
        //            _bot.SendMessage(a.ChatId, "Ошибка!");
        //        }
        //        else
        //        {
        //            string firstName = me.FirstName ?? "";
        //            string lastName = me.LastName ?? "";
        //            string userName = me.Username ?? "";
        //            var id = me.Id.ToString();
        //            var result = String.Format("FirstName: {1}{0}LastName: {2}{0}UserName: {3}{0}Id: {4}", Environment.NewLine, firstName,
        //                                          lastName, userName, id);

        //            _bot.SendMessage(a.ChatId, result);
        //        }
        //    }
        //}

        //static void Bot_OnCallbackQuery(CallbackQueryEventArgs a)
        //{
        //    if (a.CallbackQuery.Data == null)
        //    {
        //        return;
        //    }
        //    if (!Int32.TryParse(a.CallbackQuery.Data, out int n))
        //    {
        //        return;
        //    }

        //    var kb = new InlineKeyboardMarkup
        //    {
        //        InlineKeyboard = new[]
        //        {
        //            new[]
        //            {
        //                new InlineKeyboardButton
        //                {
        //                    Text = (n + 1).ToString(),
        //                    CallbackData = (n + 1).ToString()
        //                }
        //            }
        //        }
        //    };

            //_bot.EditMessageReplyMarkup(a.CallbackQuery.Message.Chat.Id, a.CallbackQuery.Message.MessageId, replyMarkup: kb);
        // }
    }
}
