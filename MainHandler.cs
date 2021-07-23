using System.Threading.Tasks;
using ChatBot.Models;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace ChatBot
{
    public static class MainHandler
    {
        public static async void OnMessage(object sender, MessageEventArgs e)
        {
            if (e.Message.Chat.Type == ChatType.Group || e.Message.Chat.Type == ChatType.Supergroup)
            {
                // some work
            }
            else if (e.Message.Chat.Type == ChatType.Private)
            {
                Program.bot.SafeSendMessageAsync(e.Message.Chat, Text.ForPrivedChat, ParseMode.Html,
                    Keyboards.InviteToChatKeyboard());
            }
            else
            {
                await Program.bot.LeaveChatAsync(e.Message.Chat);
            }
            
        }
        
        public static async void OnCallback(object sender, CallbackQueryEventArgs e)
        {
            
        }
    }
}