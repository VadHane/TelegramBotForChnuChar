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
        
        
        /// <summary>
        /// Function for safe deleting message from chat.
        /// </summary>
        /// <param name="bot">Link to bot client.</param>
        /// <param name="message">Message for deleting.</param>
        public static async Task SafeDeleteMessageAsync(this TelegramBotClient bot, Message message)
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
        
        
        /// <summary>
        /// Function for safe sinding message into group or prived chat.
        /// </summary>
        /// <param name="bot">Link to bot client.</param>
        /// <param name="chatId">Id of chat where you would like to send message.</param>
        /// <param name="text">Text of your message.</param>
        /// <param name="parseMode">Parse mode for your text.</param>
        /// <param name="keyboard">Attached keyboard to message.</param>
        public static async Task SafeSendMessageAsync(this TelegramBotClient bot,
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