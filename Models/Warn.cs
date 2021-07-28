using System;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;

namespace ChatBot.Models
{
    public class Warn : Action
    {
        /// <summary>
        /// Create new action - Warn.
        /// </summary>
        /// <param name="username">Telegram username of student.</param>
        /// <param name="name">Name of student.</param>
        /// <param name="telegramId">Unique telegram id.</param>
        public Warn(string username, string name, int telegramId)
        {
            Id = GetActionId();
            Username = username;
            Name = name;
            TelegramId = telegramId;
        }

        /// <summary>
        /// Telegram username of student.
        /// </summary>
        private string Username { get; }
        
        /// <summary>
        /// Name of student.
        /// </summary>
        private string Name { get; }
        
        /// <summary>
        /// Unique telegram id.
        /// </summary>
        private int TelegramId { get; }

        protected override async Task StartVerification()
        {
            await Program.bot.SafeSendMessageAsync(Settings.LogChat,
                $"Ви дійсно бажаєте попередити {Name}?", keyboard: Keyboards.VerificationAction(this.Id));
        }
        
        public override async Task DoAction()
        {
            await Program.bot.SafeSendMessageAsync(Settings.MainChat, Text.Warn( Username, Name, TelegramId),
                ParseMode.Html);

            Console.WriteLine($"[warn] {Name} був попереджений!");
            
            DeleteAction();
        }
    }
}