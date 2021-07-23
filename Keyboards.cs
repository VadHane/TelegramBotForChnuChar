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
    }
}