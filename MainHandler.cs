using System.Threading.Tasks;
using ChatBot.Handlers;
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
                if (Settings.Admins.Exists(adm => adm == e.Message.From.Id))
                {
                    string[] cmd = e.Message.Text.Split(" ");

                    switch (cmd[0])
                    {
                        case "/mute":
                            
                            break;
                        case "/kick":
                            
                            break;
                        default:
                            return;
                    }
                }
                else if (e.Message.Chat.Id == Settings.MainChat)
                {
                    if (e.Message.Text == "/rules")
                    {
                        // send rules into chat
                    }
                    else
                    {
                        Parse.CheckMessage(e.Message);
                    }
                }
                else if ( e.Message.Chat.Id == Settings.LogChat)
                {
                    switch (e.Message.Text)
                    {
                        case "/cmd":
                            // add word, del word, edit rules
                            break;
                        case "/help":
                            
                            break;
                        default:
                            await Program.bot.SafeDeleteMessageAsync(e.Message);
                            break;
                    }
                }
                else
                {
                    await Program.bot.LeaveChatAsync(e.Message.Chat);
                }
            }
            else if (e.Message.Chat.Type == ChatType.Private)
            {
                await Program.bot.SafeSendMessageAsync(e.Message.Chat, Text.ForPrivedChat, ParseMode.Html,
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