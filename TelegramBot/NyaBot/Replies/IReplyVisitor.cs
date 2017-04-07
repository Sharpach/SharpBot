namespace TelegramBot.NyaBot.Replies
{
    public interface IReplyVisitor<in TArgs, out TResult>
    {
        TResult VisitText(TextReply reply, TArgs args);

        TResult VisitImage(ImageReply reply, TArgs args);
    }
}