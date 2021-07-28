using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace ChatBot
{
    public static class Keyboards
    {
        public static InlineKeyboardMarkup InviteToChatKeyboard()
        {
            return new InlineKeyboardMarkup(new[]
            {
                InlineKeyboardButton.WithUrl("Приєднатись", Settings.InviteLinkIntoMainChat),
            });
        }

        public static InlineKeyboardMarkup DeletedMessage(Message message)
        {
            return new InlineKeyboardMarkup(new[]
            {
                new []
                {
                    InlineKeyboardButton.WithCallbackData("Warn", 
                        $"Admin:Warn:{message.From.Username}:{message.From.FirstName} {message.From.LastName}" +
                        $":{message.From.Id}"), 
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Mute", $"Admin:Mute:{message.From.Id}:" +
                                                                  $"{message.From.FirstName} {message.From.LastName}"),
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Kick", $"Admin:Kick:{message.From.Id}:" +
                                                                  $"{message.From.FirstName} {message.From.LastName}"),
                }
            });
        }

        public static InlineKeyboardMarkup VerificationAction(int actionId)
        {
            return new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Підтвердити", $"Action:Do:{actionId}"),
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Відмінити", $"Action:Cancel:{actionId}"),
                }
            });
        }
    }
}