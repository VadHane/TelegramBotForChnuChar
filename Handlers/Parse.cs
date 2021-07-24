using System;
using ChatBot.Models;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace ChatBot.Handlers
{
    public static class Parse
    {
        public static async void CheckMessage(Message message)
        {
            string[] text = message.Text.ToLower().Split(" ");

            foreach (var word in text)
            {
                if (Dictionary.Search(word))
                {
                    await Program.bot.SafeDeleteMessageAsync(message);
                    await Program.bot.SafeSendMessageAsync(Settings.LogChat, Text.DeletedMessage(message), ParseMode.Html,
                        Keyboards.DeletedMessage(message));
                    Console.WriteLine($"[action] Deleted message from {message.From.Username};");
                    break;
                }
            }
        }
    }
}