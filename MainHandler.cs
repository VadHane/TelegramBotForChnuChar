using System.Linq;
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
                if (e.Message.Text == null && e.Message.Type != MessageType.ChatMembersAdded)
                {
                    return;
                }

                if (e.Message.Chat.Id == Settings.MainChat)
                {
                    if (e.Message.Type == MessageType.ChatMembersAdded)
                    {
                        if (e.Message.NewChatMembers.FirstOrDefault(user => user.Id == Program.bot.BotId) == null)
                        {
                            await Rules.SendRules();
                        }
                    }
                    else if (e.Message.Text == "/rules")
                    {
                        await Rules.SendRules();
                    }
                    else if (Settings.Admins.Exists(adm => adm == e.Message.From.Id))
                    {
                        string[] cmd = e.Message.Text.Split(" ");
                        int time;

                        if (e.Message.ReplyToMessage != null)
                        {

                            switch (cmd[0])
                            {
                                case "/mute":
                                    await Program.bot.SafeDeleteMessageAsync(e.Message);
                                    if (cmd.Length > 1 && int.TryParse(cmd[1], out time))
                                    {
                                        Action.AddAction(new Mute(
                                            e.Message.ReplyToMessage.From.FirstName + " " +
                                            e.Message.ReplyToMessage.From.LastName,
                                            e.Message.ReplyToMessage.From.Id, time));
                                    }
                                    else
                                    {
                                        await Program.bot.SafeSendMessageAsync(Settings.LogChat,
                                            "Я не зміг обмежити користувача: невірно введено час!");
                                    }

                                    break;
                                case "/kick":
                                    await Program.bot.SafeDeleteMessageAsync(e.Message);
                                    Action.AddAction(new Kick(
                                        e.Message.ReplyToMessage.From.FirstName + " " +
                                        e.Message.ReplyToMessage.From.LastName,
                                        e.Message.ReplyToMessage.From.Id));
                                    break;
                                case "/warn":
                                    await Program.bot.SafeDeleteMessageAsync(e.Message);
                                    Action.AddAction(new Warn(
                                        e.Message.ReplyToMessage.From.Username,
                                        e.Message.ReplyToMessage.From.FirstName + " " +
                                        e.Message.ReplyToMessage.From.LastName,
                                        e.Message.ReplyToMessage.From.Id));
                                    break;
                                case "/del":
                                    await Program.bot.SafeDeleteMessageAsync(e.Message.ReplyToMessage);
                                    await Program.bot.SafeDeleteMessageAsync(e.Message);
                                    break;
                            }
                        }
                        else
                        {
                            switch (cmd[0])
                            {
                                case "/add":
                                    await Program.bot.SafeDeleteMessageAsync(e.Message);
                                
                                    if (cmd.Length < 2) return;

                                    await Dictionary.Add(cmd[1]);
                                    break;
                                case "/delete":
                                    await Program.bot.SafeDeleteMessageAsync(e.Message);
                                    
                                    if (cmd.Length < 2) return;

                                    await Dictionary.Delete(cmd[1]);
                                    break;
                            }
                        }
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
                        case "/help":
                            await HelpMessage.SendMessage();
                            await Program.bot.SafeDeleteMessageAsync(e.Message);
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
            string[] cmd = e.CallbackQuery.Data.Split(":");

            switch (cmd[0])
            {
                case "Admin":
                    Admin.ParseCommandFromKeyboard(cmd);
                    break;
                case "Action":
                    await ActionsHandler.ParseCommand(cmd, e.CallbackQuery.Message);
                    break;
            }
        }
    }
}