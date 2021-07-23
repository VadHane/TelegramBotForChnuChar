using System;
using System.Threading;
using Telegram.Bot;

namespace ChatBot
{
    static class Program
    {
        public static readonly TelegramBotClient bot = new TelegramBotClient(PrivateSettings.TOKEN);
        
        static void Main(string[] args)
        {
            bot.OnMessage += MainHandler.OnMessage;
            bot.OnCallbackQuery += MainHandler.OnCallback;
            bot.StartReceiving();
            Thread.Sleep(int.MaxValue);
        }
    }
}