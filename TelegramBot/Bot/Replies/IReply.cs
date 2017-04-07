namespace TelegramBot.NyaBot.Replies
{
    public interface IReply
    {
        TResult AcceptVisitor<TArgs, TResult>(IReplyVisitor<TArgs, TResult> visitor, TArgs args);
    }
}