using Telegram.Bot.Types;

namespace ChatBot.Models
{
    public static class Text
    {
        public const string ForPrivedChat = "Я працюю виключно в <b>чаті студентів ЧНУ</b>!";

        public static string DeleteWord(Word word)
        {
            return "<b>Я видалив слово із словника. </b> \n" +
                   $"Слово - <em>{word.Text}</em>.";
        }
        
        public static string AddWord(Word word)
        {
            return "<b>Я добавив нове слово в словник. </b> \n" +
                   $"Слово - <em>{word.Text}</em>.";
        }

        public static string DeletedMessage(Message message)
        {
            return "<b>Я видалив повідомлення! \nІнформація про повідомлення:</b>" +
                   $"\nВідправник: {message.From.FirstName} {message.From.LastName} " +
                   $"[<a href=\"https://t.me/{message.From.Username}\">Profile</a>]" +
                   $"\nУнікальний ідентифікатор відправника: {message.From.Id}" +
                   $"\nЧат: {message.Chat.FirstName} {message.Chat.LastName}" +
                   $"\nЧас відправлення: {message.Date.TimeOfDay}" +
                   $"\nПовідомлення: {message.Text}";
        }
        
        public static string CantDeleteMessage(Message message)
        {
            return "<b>Я не зміг видалити повідомлення! \nІнформація про повідомлення:</b>" +
                   $"\nВідправник: {message.From.FirstName} {message.From.LastName} " +
                   $"[<a href=\"https://t.me/{message.From.Username}\">Profile</a>]" +
                   $"\nЧат: {message.Chat.FirstName} {message.Chat.LastName}" +
                   $"\nЧас відправлення: {message.Date.TimeOfDay}" +
                   $"\nПовідомлення: {message.Text}" +
                   "\n\n\n<em>Можлива причина цієї помилки - повідомлення було видалено скоріше, ніж це зміг зробити я!</em>";
        }
    }
}