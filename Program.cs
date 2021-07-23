﻿using System;
using Telegram.Bot;

namespace ChatBot
{
    class Program
    {
        public static readonly TelegramBotClient bot = new TelegramBotClient(PrivateSettings.TOKEN);
        
        static void Main(string[] args)
        {
            bot.OnMessage += MainHandler.OnMessage;
            bot.OnCallbackQuery += MainHandler.OnCallback;
        }
    }
}