using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace ChatBot.Models
{
    public static class SafeActions
    {
        public static async void SafeDeleteMessageAsync(this TelegramBotClient bot, Message message)
        {
            try
            {
                await Program.bot.DeleteMessageAsync(message.Chat, message.MessageId);
            }
            catch
            {
                Console.WriteLine($"[Error] I can`t delete message! Check log-chat!");
                Program.bot.SafeSendMessageAsync(Settings.LogChat, Text.CantDeleteMessage(message), ParseMode.Html);
            }
        }

        public static async void SafeSendMessageAsync(this TelegramBotClient bot,
            ChatId chatId, string text, ParseMode parseMode = ParseMode.Default, IReplyMarkup keyboard = null)
        {
            try
            {
                await Program.bot.SendTextMessageAsync(chatId, text, parseMode, replyMarkup: keyboard);
            }
            catch
            {
                Console.WriteLine($"[Error] I can`t send message! (chatId - {chatId}, message - {text});");
            }
        }
    }
}