namespace TelegramBot.Bot.Replies
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
