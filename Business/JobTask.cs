using Roleplay.Engine;
using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roleplay.Business
{
    public class JobTask
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public IList<int> Rewards { get; set; }
        public Schemas.Action Action { get; set; }

        public bool Completed { get; set; } = false;

        public JobTask(string name, IList<int> rewards, Schemas.Action action)
        {
            this.Name = name;

            this.Rewards = rewards;

            this.Action = action;
        }

        [Roleplay.Events.Action.Common.ClientDidAction]
        public void OnDidAction(Roleplay.Schemas.Action action, IClient client)
        {
            Log.Info("test");
        }

    }
}
