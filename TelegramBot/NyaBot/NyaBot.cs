using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TelegramBot.API;
using TelegramBot.API.Models;
using TelegramBot.NyaBot.Args;
using TelegramBot.NyaBot.Commands;
using TelegramBot.NyaBot.Replies;
using TelegramBot.NyaBot.Types;
using TelegramBot.NyaBot.Updates;

namespace TelegramBot.NyaBot
{
    public class NyanBot
    {
        private int _updateOffset = 0;
        private readonly ApiClient _api;
        private readonly ICommandInvoker _invoker;
        private readonly IUpdatesProvider _updatesProvider;
        private readonly IReplySender _replySender;

        public NyanBot(ApiClient api, ICommandInvoker invoker, IUpdatesProvider updatesProvider, IReplySender replySender)
        {
            _api = api;
            _invoker = invoker;
            _updatesProvider = updatesProvider;
            _replySender = replySender;
        }

        internal async Task Start()
        {
            IsRunning = true;
            await UpdatesThread();
        }

        internal void Stop()
        {
            IsRunning = false;
        }

        internal bool IsRunning { get; private set; }


  //      internal void EditMessageReplyMarkup(string chatId = null, int messageId = 0, string inlineMessageId = null,
  //                                         InlineKeyboardMarkup replyMarkup = null)
  //      {
  //          var edit = new EditMarkupData
  //          {
  //              ChatId = chatId,
  //              MessageId = messageId,
  //              InlineMessageId = inlineMessageId,
  //              ReplyMarkup = replyMarkup
  //          };

  //          _api.SendRequest("editMessageReplyMarkup", edit);
  //      }

  //      internal async Task EditMessageReplyMarkupAsync(string chatId = null, int messageId = 0, string inlineMessageId = null,
  //                                         InlineKeyboardMarkup replyMarkup = null)
  //      {
  //          var edit = new EditMarkupData
  //          {
  //              ChatId = chatId,
  //              MessageId = messageId,
  //              InlineMessageId = inlineMessageId,
  //              ReplyMarkup = replyMarkup
  //          };

  //          await _api.SendRequestAsync("editMessageReplyMarkup", edit);
  //      }

  //      internal void SendPhoto(string chatId, string photo, string caption = null, bool disableNotification = false,
  //                            int replyToMessageId = 0, object replyMarkup = null)
  //      {
  //          var photow = new PhotoToSend
  //          {
  //              ChatId = chatId,
  //              Photo = photo,
  //              Caption = caption,
  //              DisableNotification = disableNotification,
  //              ReplyToMessageId = replyToMessageId,
  //              ReplyMarkup = replyMarkup
  //          };

  //          _api.SendRequest("sendPhoto", photow);
		//}

  //      internal async Task SendPhotoAsync(string chatId, string photo, string caption = null, bool disableNotification = false,
  //                    int replyToMessageId = 0, object replyMarkup = null)
  //      {
  //          var photow = new PhotoToSend
  //          {
  //              ChatId = chatId,
  //              Photo = photo,
  //              Caption = caption,
  //              DisableNotification = disableNotification,
  //              ReplyToMessageId = replyToMessageId,
  //              ReplyMarkup = replyMarkup
  //          };

  //          await _api.SendRequestAsync("sendPhoto", photow);
  //      }

  //      internal void SendSticker(string chatId, string sticker, bool disableNotification = false,
  //                              int replyToMessageId = 0, object replyMarkup = null)
  //      {
  //          var stickerw = new StickerToSend
  //          {
  //              ChatId = chatId,
  //              Sticker = sticker,
  //              DisableNotification = disableNotification,
  //              ReplyToMessageId = replyToMessageId,
  //              ReplyMarkup = replyMarkup
  //          };

  //          _api.SendRequest("sendSticker", stickerw);
  //      }

  //      internal async Task SendStickerAsync(string chatId, string sticker, bool disableNotification = false,
  //                      int replyToMessageId = 0, object replyMarkup = null)
  //      {
  //          var stickerw = new StickerToSend
  //          {
  //              ChatId = chatId,
  //              Sticker = sticker,
  //              DisableNotification = disableNotification,
  //              ReplyToMessageId = replyToMessageId,
  //              ReplyMarkup = replyMarkup
  //          };

  //          await _api.SendRequestAsync("sendSticker", stickerw);
  //      }

  //      internal void SendChatAction(string chatId, ChatAction action)
  //      {
  //          string actionString = String.Empty;

  //          switch (action)
  //          {
  //              case ChatAction.Typing:
  //                  actionString = "typing";
  //                  break;
  //              case ChatAction.UploadPhoto:
  //                  actionString = "upload_photo";
  //                  break;
  //              case ChatAction.UploadAudio:
  //                  actionString = "upload_audio";
  //                  break;
  //              case ChatAction.UploadDocument:
  //                  actionString = "upload_document";
  //                  break;
  //              case ChatAction.UploadVideo:
  //                  actionString = "upload_video";
  //                  break;
  //              case ChatAction.FindLocation:
  //                  actionString = "find_location";
  //                  break;
  //          }

  //          var actionw = new ChatActionToSend
  //          {
  //              ChatId = chatId,
  //              Action = actionString
  //          };

  //          _api.SendRequest("sendChatAction", actionw);
  //      }

  //      internal async Task SendChatActionAsync(string chatId, ChatAction action)
  //      {
  //          string actionString = String.Empty;

  //          switch (action)
  //          {
  //              case ChatAction.Typing:
  //                  actionString = "typing";
  //                  break;
  //              case ChatAction.UploadPhoto:
  //                  actionString = "upload_photo";
  //                  break;
  //              case ChatAction.UploadAudio:
  //                  actionString = "upload_audio";
  //                  break;
  //              case ChatAction.UploadDocument:
  //                  actionString = "upload_document";
  //                  break;
  //              case ChatAction.UploadVideo:
  //                  actionString = "upload_video";
  //                  break;
  //              case ChatAction.FindLocation:
  //                  actionString = "find_location";
  //                  break;
  //          }

  //          var actionw = new ChatActionToSend
  //          {
  //              ChatId = chatId,
  //              Action = actionString
  //          };

  //          await _api.SendRequestAsync("sendChatAction", actionw);
  //      }

  //      // normal versions
  //      internal void SendMessage(long chatId, string text, bool disableWebPagePreview = false,
  //                              bool disableNotification = false, int replyToMessageId = 0, object replyMarkup = null) =>
  //      SendMessage(chatId.ToString(), text, disableWebPagePreview, disableNotification, replyToMessageId, replyMarkup);

  //      internal void EditMessageText(long chatId, string text, int messageId = 0, string inlineMessageId = null,
  //                           bool disableWebPagePreview = false, object replyMarkup = null) =>
  //      EditMessageText(chatId.ToString(), text, messageId, inlineMessageId, disableWebPagePreview, replyMarkup);

  //      internal void EditMessageReplyMarkup(long chatId = 0, int messageId = 0, string inlineMessageId = null,
  //                                         InlineKeyboardMarkup replyMarkup = null) =>
  //      EditMessageReplyMarkup(chatId.ToString(), messageId, inlineMessageId, replyMarkup);

  //      internal void SendPhoto(long chatId, string photo, string caption = null, bool disableNotification = false,
  //                            int replyToMessageId = 0, object replyMarkup = null) =>
  //      SendPhoto(chatId.ToString(), photo, caption, disableNotification, replyToMessageId, replyMarkup);

  //      internal void SendSticker(long chatId, string sticker, bool disableNotification = false,
  //                              int replyToMessageId = 0, object replyMarkup = null) =>
  //      SendSticker(chatId.ToString(), sticker, disableNotification, replyToMessageId, replyMarkup);

  //      internal void SendChatAction(long chatId, ChatAction action) =>
  //      SendChatAction(chatId.ToString(), action);

  //      //async versions
  //      internal async Task SendMessageAsync(long chatId, string text, bool disableWebPagePreview = false,
  //                              bool disableNotification = false, int replyToMessageId = 0, object replyMarkup = null) =>
  //      await SendMessageAsync(chatId.ToString(), text, disableWebPagePreview, disableNotification, replyToMessageId, replyMarkup);

  //      internal async Task EditMessageTextAsync(long chatId, string text, int messageId = 0, string inlineMessageId = null,
  //                           bool disableWebPagePreview = false, object replyMarkup = null) =>
  //      await EditMessageTextAsync(chatId.ToString(), text, messageId, inlineMessageId, disableWebPagePreview, replyMarkup);

  //      internal async Task EditMessageReplyMarkupAsync(long chatId = 0, int messageId = 0, string inlineMessageId = null,
  //                                         InlineKeyboardMarkup replyMarkup = null) =>
  //      await EditMessageReplyMarkupAsync(chatId.ToString(), messageId, inlineMessageId, replyMarkup);

  //      internal async Task SendPhotoAsync(long chatId, string photo, string caption = null, bool disableNotification = false,
  //                            int replyToMessageId = 0, object replyMarkup = null) =>
  //      await SendPhotoAsync(chatId.ToString(), photo, caption, disableNotification, replyToMessageId, replyMarkup);

  //      internal async Task SendStickerAsync(long chatId, string sticker, bool disableNotification = false,
  //                              int replyToMessageId = 0, object replyMarkup = null) =>
  //      await SendStickerAsync(chatId.ToString(), sticker, disableNotification, replyToMessageId, replyMarkup);

  //      internal async Task SendChatActionAsync(long chatId, ChatAction action) =>
  //      await SendChatActionAsync(chatId.ToString(), action);
  //      // end

        private async Task UpdatesThread()
        {
            Logger.LogMessage("Бот запущен.");
            while (IsRunning)
            {
                var updates = await GetUpdates();

                foreach (var update in updates)
				{
                    if (update.CallbackQuery != null && OnCallbackQuery != null)
                    {
                        var args = new CallbackQueryEventArgs
                        {
                            CallbackQuery = update.CallbackQuery
                        };

                        OnCallbackQuery(args);
                    }

                    if (update.Message != null)
                    {
                        var args = new TelegramMessageEventArgs
                        {
                            ChatId = update.Message.Chat.Id,
                            MessageId = update.Message.MessageId,
                            From = update.Message.From,
                            Message = update.Message
                        };

                        var results = await _invoker.Invoke(args);
                        foreach (var result in results)
                        {
                            await _replySender.Send(result, args.ChatId);
                            await Task.Delay(300);
                        }
                        
                    }
                    if (update.InlineQuery != null && OnInlineQuery != null)
                    {
                        var args = new InlineQueryEventArgs
                        {
                            InlineQuery = update.InlineQuery
                        };

                        OnInlineQuery(args);
                    }
                }

                await Task.Delay(1000);
            }
        }

        private Task<ICollection<Update>> GetUpdates()
        {
            try
            {
                return _updatesProvider.GetUpdates();
            }
            catch (Exception ex)
            {
                IsRunning = false;
                if (ex is WebException || ex is JsonException)
                {
                    Logger.LogFatal(ex);
                    return Task.FromResult((ICollection<Update>)new Update[]{});
                }
                throw;
            }
        }

        internal event InlineQueryHandler OnInlineQuery;

        internal event CallbackQueryHandler OnCallbackQuery;
	}
}