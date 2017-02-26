﻿/*
 * Осторожно, весь бот расположен в одном файле, чтобы не плодить кучу файлов для личных целей
 * Также завёл отдельный неймспейс под это дело, а то у других будет куча ненужных для них классов
*/

using System;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TelegramBot.API_Classes;

namespace TelegramBot.NyaBot
{
	public class NyanBot
	{
		private const string BASE_API_ADDRESS = @"https://api.telegram.org/bot";

		WebClient client = new WebClient();
		private string token = String.Empty;
		private bool isRun = false;
		private int updateOffset = 0;

		public NyanBot(string token)
		{
			this.token = token;
		}

		public void Start()
		{
			isRun = true;
			UpdatesThread();
		}

		public void Stop()
		{
			isRun = false;
		}

		public bool IsRun => isRun;

		public void SendMessage(long chatId, string text, int replayToMessageId = 0)
		{
			if (text.Length < 1)
			{
				return;
			}

			try
			{
				client.DownloadString($"{BASE_API_ADDRESS}{token}/sendMessage?chat_id={chatId}&text={text}&reply_to_message_id={replayToMessageId}");
			}
			catch (WebException e)
			{
				Console.WriteLine(e);
			}
		}

        public async Task SendMessageAsync(long chatId, string text, int replayToMessageId = 0)
        {
            if (text.Length < 1)
            {
                return;
            }

            try
            {
                await client.DownloadStringTaskAsync($"{BASE_API_ADDRESS}{token}/sendMessage?chat_id={chatId}&text={text}&reply_to_message_id={replayToMessageId}");
            }
            catch (WebException e)
            {
                Console.WriteLine(e);
            }
        }

		public void SendPhoto(long chatId, string url, string caption = "", int replayToMessageId = 0)
		{
			try
			{
				client.DownloadString($"{BASE_API_ADDRESS}{token}/sendPhoto?chat_id={chatId}&photo={url}&caption={caption}&reply_to_message_id={replayToMessageId}");
			}
			catch (WebException e)
			{
				Console.WriteLine(e);
			}
		}

        public async Task SendPhotoAsync(long chatId, string url, string caption = "", int replayToMessageId = 0)
        {
            try
            {
                await client.DownloadStringTaskAsync($"{BASE_API_ADDRESS}{token}/sendPhoto?chat_id={chatId}&photo={url}&caption={caption}&reply_to_message_id={replayToMessageId}");
            }
            catch (WebException e)
            {
                Console.WriteLine(e);
            }
        }

		public void SendSticker(long chatId, string sticker, int replayToMessageId = 0)
		{
			try
			{
				client.DownloadString($"{BASE_API_ADDRESS}{token}/sendSticker?chat_id={chatId}&sticker={sticker}&reply_to_message_id={replayToMessageId}");
			}
			catch (WebException e)
			{
				Console.WriteLine(e);
			}
		}

        public async Task SendStickerAsync(long chatId, string sticker, int replayToMessageId = 0)
        {
            try
            {
                await client.DownloadStringTaskAsync($"{BASE_API_ADDRESS}{token}/sendSticker?chat_id={chatId}&sticker={sticker}&reply_to_message_id={replayToMessageId}");
            }
            catch (WebException e)
            {
                Console.WriteLine(e);
            }
        }

		private void UpdatesThread()
		{
			while (isRun)
			{
				string jsonText = null;
				try
				{
					jsonText = client.DownloadString($"{BASE_API_ADDRESS}{token}/getUpdates?offset={updateOffset}");
				}
				catch (WebException e)
				{
					Console.WriteLine(e);
				}
				if (jsonText == null)
				{
					continue;
				}


				Response response = null;
				try
				{
					response = JsonConvert.DeserializeObject<Response>(jsonText);
				}
				catch (JsonException e)
				{
					Console.WriteLine(e);
					continue;
				}

				if (!response.Success)
				{
					continue;
				}

				foreach (var update in response.Updates)
				{
					updateOffset = update.UpdateId + 1;

					if (update.Message != null && OnMessage != null)
					{
						var args = new TelegramMessageEventArgs
						{
							From = update.Message.From,
							Message = update.Message
						};

						OnMessage(args);
					}
				}

				System.Threading.Thread.Sleep(1000);
			}
		}

		internal event TelegramMessageHandler OnMessage;
	}

	// Handlers
	internal delegate void TelegramMessageHandler(TelegramMessageEventArgs args);

	// EventArgs
	internal class TelegramMessageEventArgs : EventArgs
	{
		public User From { get; set; }
		public Message Message { get; set; }
	}
}