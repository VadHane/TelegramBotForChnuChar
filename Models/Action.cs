using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ChatBot.Models
{
    public abstract class Action
    {
        /// <summary>
        /// Unique id for this action.
        /// </summary>
        protected int Id { get; init; }

        /// <summary>
        /// Function for starting verification this action.
        /// </summary>
        protected abstract Task StartVerification();

        /// <summary>
        ///  This function do main task for this action.
        /// </summary>
        public abstract Task DoAction();
        

        /// <summary>
        /// Delete this action from action`s list.
        /// </summary>
        public void DeleteAction()
        {
            Actions.RemoveAll(action => action.Id == this.Id);
        }
        
        
        
        
        private static int ActionId = 0;
        private static List<Action> Actions = new List<Action>();

        /// <summary>
        /// Get action by action`s unique id.
        /// </summary>
        /// <param name="actionId">Action`s unique id.</param>
        public static Action GetAction(int actionId)
        {
            return Actions.FirstOrDefault(action => action.Id == actionId);
        }

        /// <summary>
        /// Add action in action list.
        /// </summary>
        public static void AddAction(Action action)
        {
            Actions.Add(action);
            action.StartVerification();
        }

        protected static int GetActionId()
        {
            if (Actions.Count == 0)
            {
                ActionId = 0;
            }

            return ActionId++;
        }
    }
}