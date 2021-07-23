using Telegram.Bot.Types;

namespace ChatBot.Models
{
    public static class Text
    {
        public const string ForPrivedChat = "Я працюю виключно в <b>чаті студентів ЧНУ</b>!";

        public static string CantDeleteMessage(Message message)
        {
            return $"<b>Я не зміг видалити повідомлення! \nІнформація про повідомлення:</b>" +
                   $"\nВідправник: {message.From.FirstName} {message.From.LastName} " +
                   $"[<a href=\"https://t.me/{message.From.Username}\">Profile</a>]" +
                   $"\nЧат: {message.Chat.FirstName} {message.Chat.LastName}" +
                   $"\nЧас відправлення: {message.Date.TimeOfDay}" +
                   $"\nПовідомлення: {message.Text}" +
                   $"\n\n\n<em>Можлива причина цієї помилки - повідомлення було видалено скоріше, ніж це зміг зробити я!</em>";
        }
    }
}