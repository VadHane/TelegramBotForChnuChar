using System;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace ChatBot.Models
{
    public static class Rules // Singleton
    {
        private static Message message;

        private static string text = 
            "Привіт! \nЦей чат має деякі <b>обмеження, викликані здоровим глуздом та культурою спілкування</b>:" + 
            "\n 1. Не вживати <b>нецензурну лексику</b> (<em>аудіо та відеоматеріали подібного змісту</em>)" + 
            "\n 2. Не кидати посилання <b>без погодження з адмінами</b>. <em>Бо перетворимо цей чат у великий OLX</em>" + 
            "\n 3. Не <b>ображати</b> нікого своїм висловлюванням чи іншими діями. \n\n\n" + 
            "А зараз хутчіш пиши свою спеціальність та починай пошук нових друзів!";
        
        public static async Task SendRules()
        {
            if (message != null)
            {
                await Program.bot.SafeDeleteMessageAsync(message);
            }
            
            message = Program.bot.SendTextMessageAsync(
                Settings.MainChat, text, ParseMode.Html).Result;
        }
    }
}