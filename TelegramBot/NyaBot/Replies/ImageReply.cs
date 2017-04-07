using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.NyaBot.Replies
{
    public class ImageReply : IReply
    {
        public byte[] Image { get; }

        public ImageReply(byte[] image)
        {
            Image = image;
        }

        public TResult AcceptVisitor<TArgs, TResult>(IReplyVisitor<TArgs, TResult> visitor, TArgs args)
        {
            return visitor.VisitImage(this, args);
        }
    }
}
