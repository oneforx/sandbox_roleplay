using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sandbox;

namespace Roleplay.Models
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


    public class Action : Table, IAction
    {
        public string Name { get; set; }

        public Action(string name) : base("action")
        {
            Name = name;
        }

        public event ActionHandler ActionCompleted;

        protected virtual void OnActionCompleted(ActionEventArgs e)
        {
            ActionCompleted?.Invoke(this, e);
        }

        [Events.Action.Common.ClientDidAction]
        public void OnDidAction(Action action, IClient client)
        {
            Log.Info(action);
        }
    }
}
