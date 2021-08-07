using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace ChatBot.Models
{
    public static class HelpMessage // singleton
    {
        private static Message message;

        private static string text = "<b>Список доступних команд та їх перегрузки:</b> \n" +
                                     "\nКоманда <b>/warn</b>: \n" +
                                     "Попереджує гравця про незадовільну поведінку, не накладає жодних обмежень на користувача.\n" +
                                     "Використовується з клавіатури видаленого повідомлення або з прикріпленим повідомленням \n\n" +
                                     "\nКоманда <b>/mute [час]</b>: \n" +
                                     "Забороняє користувачу писати що-небудь в чат. \n" +
                                     "Використовіється з клавіатури або з прикріпленим повідомленням \n" +
                                     "[ВАЖЛИВО] З прикрімленим повідомленням потрібно явно вказати час: /mute 20 \n\n" +
                                     "\nКоманда <b>/kick</b>: \n" +
                                     "Вилучає гравця із групи. \n" +
                                     "Використовується з клавіатури видаленого повідомлення або з прикріпленим повідомленням \n\n" +
                                     "\nКоманда <b>/add [слово]</b>: \n" +
                                     "Додає слово до словника виключених слів." +
                                     "Використовується без прикріпленого повідомлення.\n\n" +
                                     "\nКоманда <b>/delete [слово]</b>: \n" +
                                     "Видаляє слово зі словника виключених слів." +
                                     "Використовується без прикріпленого повідомлення.\n\n" +
                                     "\nКоманда <b>/rules</b>: \n" +
                                     "Відправляє в чат правила групи. \n" +
                                     "Працює без прикріпленого повідомлення.\n\n\n" +
                                     "<b>[ВАЖЛИВО] Усі ці команди працюють тільки в головному чаті!</b>";

        public static async Task SendMessage()
        {
            if (message != null)
            {
                await Program.bot.SafeDeleteMessageAsync(message);
            }
            
            message = Program.bot.SendTextMessageAsync(
                Settings.LogChat, text, ParseMode.Html).Result;
        }
    }
}