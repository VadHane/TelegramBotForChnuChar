using System.Threading.Tasks;
using ChatBot.Models;
using Telegram.Bot.Types;

namespace ChatBot.Handlers
{
    public static class ActionsHandler
    {
        /// <summary>
        /// Function for correct parse some command from action`s keyboard.
        /// </summary>
        /// <param name="cmd">Array of string (command).</param>
        /// <param name="message">Message where was attached action`s keyboard.</param>
        public static async Task ParseCommand(string[] cmd, Message message)
        {
            switch (cmd[1])
            {
                case "Do":
                    await Program.bot.SafeDeleteMessageAsync(message);
                    var action = Action.GetAction(int.Parse(cmd[2]));
                    await action.DoAction();
                    break;
                case "Cancel":
                    Action.GetAction(int.Parse(cmd[2])).DeleteAction();
                    await Program.bot.SafeDeleteMessageAsync(message);
                    break;
            }
        }
    }
}