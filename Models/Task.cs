using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Roleplay.Systems;

namespace Roleplay.Models
{

    public class SpecifiqueAction
    {
        public string Name { get; set; }

        public string TypeName { get; set; }

        public SpecifiqueAction() { }

        public SpecifiqueAction(string name, Type typeName)
        {
            Name = name;
            TypeName = typeName.Name;
        }
    }

    public class Task : Table
    {
        public string Name { get; set; }

        public string Title { get; set; }

        public SpecifiqueAction Action { get; set; }

        public int CurrentActionCount { get; set; } = 0;
        
        public int TriggerActionCount { get; set; }

        [JsonIgnore]
        public string Description => Action.Name + " " + TriggerActionCount + " " + Action.TypeName;


        public Task() { }

        public Task(string name, SpecifiqueAction action, int triggerActionCount) : base("business_task")
        {
            Name = name;
            Action = action;
            TriggerActionCount = triggerActionCount;
        }
    }
}
