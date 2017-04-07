using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.API;
using TelegramBot.NyaBot.Types;

namespace TelegramBot.NyaBot.Replies
{
    class ReplySender : IReplyVisitor<long, Task>, IReplySender
    {
        private readonly ApiClient _client;

        public ReplySender(ApiClient client)
        {
            _client = client;
        }

        public Task Send(IReply reply, long chatId)
        {
            return reply.AcceptVisitor(this, chatId);
        }

        public async Task VisitText(TextReply reply, long chatId)
        {
            await _client.SendRequestAsync<object>("sendMessage", new MessageToSend
            {
                ChatId = chatId.ToString(),
                Text = reply.Text,
                DisableWebPagePreview = false,
                DisableNotification = false,
                ReplayToMessageId = 0,
                ReplyMarkup = null
            });
        }

        public async Task VisitImage(ImageReply reply, long chatId)
        {
            await _client.SendFile<object>("sendPhoto", chatId, reply.Image);
                
        }
    }
}
