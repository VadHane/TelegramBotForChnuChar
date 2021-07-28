using System;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;

namespace ChatBot.Models
{
    public class Kick : Action
    {
        /// <summary>
        /// Create new action - Kick.
        /// </summary>
        /// <param name="name">Name of student.</param>
        /// <param name="telegramId">Unique telegram id.</param>
        public Kick(string name, int telegramId)
        {
            Id = GetActionId();
            TelegramId = telegramId;
            Name = name;
        }
        
        /// <summary>
        /// Unique telegram id.
        /// </summary>
        private int TelegramId { get;  }
        
        /// <summary>
        /// Name of student.
        /// </summary>
        private string Name { get;  }
        
        protected override async Task StartVerification()
        {
            await Program.bot.SafeSendMessageAsync(Settings.LogChat,
                $"Ви дійсно бажаєте видалити {Name} з групи?",
                keyboard: Keyboards.VerificationAction(this.Id));
        }

        public override async Task DoAction()
        {
            await Program.bot.SafeDeleteMembersAsync(TelegramId);
            await Program.bot.SafeSendMessageAsync(Settings.MainChat, "<b>Вчіться на чужих помилках!</b>",
                ParseMode.Html);

            Console.WriteLine($"[kick] I delete members from chat. [name - {Name}]");
            await Program.bot.SafeSendMessageAsync(Settings.LogChat, $"Я видалив {Name} з чату!");
            
            DeleteAction();
        }
    }
}