using Newtonsoft.Json;

namespace TelegramBot.NyaBot.Types
{
    public class ChatActionToSend
    {
        [JsonProperty("chat_id")]
        public string ChatId { get; set; }

        [JsonProperty("action")]
        public string Action { get; set; }
    }
}
