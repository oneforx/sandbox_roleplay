using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roleplay.Engine
{
    public interface IAction
    {
        public string Name { get; set; }
    }



    public delegate void ActionHandler(object sender, ActionEventArgs e);

    public class ActionEventArgs : EventArgs
    {
        public string ActionName { get; }

        public ActionEventArgs(string actionName)
        {
            ActionName = actionName;
        }
    }


    public class Action : IAction
    {
        public string Name { get; set; }

        public Action(string name)
        {
            this.Name = name;
        }

        public event ActionHandler ActionCompleted;

        protected virtual void OnActionCompleted(ActionEventArgs e)
        {
            ActionCompleted?.Invoke(this, e);
        }
    }

    public class EatAction : Action
    {
        public EatAction() : base("action.eat")
        {

        }
    }
    
}
