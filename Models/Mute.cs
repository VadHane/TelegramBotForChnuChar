using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Args;

namespace ChatBot.Models
{
    public class Mute : Action
    {
        /// <summary>
        /// Create new action - Mute.
        /// </summary>
        /// <param name="name">Name of student.</param>
        /// <param name="telegramId">Unique telegram id.</param>
        public Mute(string name, int telegramId)
        {
            Id = GetActionId();
            Name = name;
            TelegramId = telegramId;
        }

        public Mute(string name, int telegramId, int time)
        {
            Id = GetActionId();
            Name = name;
            TelegramId = telegramId;
            Time = time;
        }
        
        /// <summary>
        /// Name of student.
        /// </summary>
        private string Name { get; }
        
        /// <summary>
        /// Unique telegram id.
        /// </summary>
        private int TelegramId { get; }
        
        /// <summary>
        /// Number of minutes for command mute.
        /// </summary>
        private int Time;
        
        protected override async Task StartVerification()
        {
            await Program.bot.SafeSendMessageAsync(Settings.LogChat,
                $"Ви дійсно бажаєте обмежити доступ до відправки повідомлень для {Name}?",
                keyboard: Keyboards.VerificationAction(this.Id));
        }

        public override async Task DoAction()
        {
            if (Time != default)
            {
                if (Program.bot.SafeEditUserPermissionsAsync(TelegramId, false, Time).Result)
                {
                    await Program.bot.SafeSendMessageAsync(Settings.LogChat,
                        $"Ви успішно обмежили можливість надсилання повідомлень для {Name} на {Time} хвилин");
                    await Program.bot.SafeSendMessageAsync(Settings.MainChat,
                        $"{Name}, давай пограємо в мовчанку? \nПротримаєшся {Time} хвилин?");
                    DeleteAction();
                }
                else
                {
                    await Program.bot.SafeSendMessageAsync(Settings.LogChat,
                        $"Я не зміг обмежити цього користувача!");
                }
                return;
            }
            
            
            int step = 1;
            var wait = new ManualResetEvent(false);

            EventHandler<MessageEventArgs> func = async (object sender, MessageEventArgs e) =>
            {
                if (e.Message.Chat.Id != Settings.LogChat) return;

                if (step == 1)
                {
                    step++;
                    if (int.TryParse(e.Message.Text, out Time))
                    {
                        if (Program.bot.SafeEditUserPermissionsAsync(TelegramId, false, Time).Result)
                        {
                            await Program.bot.SafeSendMessageAsync(Settings.LogChat,
                                $"Ви успішно обмежили можливість надсилання повідомлень для {Name} на {Time} хвилин");
                            await Program.bot.SafeSendMessageAsync(Settings.MainChat,
                                $"{Name}, давай пограємо в мовчанку? \nПротримаєшся {Time} хвилин?");
                            DeleteAction();
                        }
                        else
                        {
                            await Program.bot.SafeSendMessageAsync(Settings.LogChat,
                                $"Я не зміг обмежити цього користувача!");
                        }
                    }
                    else
                    {
                        step--;
                        await Program.bot.SafeSendMessageAsync(Settings.LogChat,
                            $"Введіть коректний час: ");
                    }
                }
                
            };

            await Program.bot.SafeSendMessageAsync(Settings.LogChat, "Введіть скільки хвилин діятиме це обмеження: ");
            Program.bot.OnMessage += func;
            wait.WaitOne();
            Program.bot.OnMessage -= func;
        }
    }
}