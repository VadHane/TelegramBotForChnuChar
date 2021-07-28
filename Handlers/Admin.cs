using ChatBot.Models;
using Action = ChatBot.Models.Action;

namespace ChatBot.Handlers
{
    public static class Admin
    {
        /// <summary>
        /// Function for correct parse coomand from attached keyboard to message about delete some message.
        /// </summary>
        /// <param name="cmd">Array of string (command).</param>
        public static void ParseCommandFromKeyboard(string[] cmd)
        {
            switch (cmd[1])
            {
                case "Warn":
                    Action.AddAction(new Warn(cmd[2], cmd[3], int.Parse(cmd[4])));
                    break;
                case "Mute":
                    Action.AddAction(new Mute(cmd[3], int.Parse(cmd[2])));
                    break;
                case "Kick":
                    Action.AddAction(new Kick(cmd[3], int.Parse(cmd[2])));
                    break;
            }
        }
    }
}