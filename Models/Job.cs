using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roleplay.Models
{
    public class Job : Table
    {
        public string Name { get; set; }
        public Job(string name) : base("business_job")
        {
            Event.Register(this);
            Name = name;
        }

        public Task CreateTask(Database database, Task businessTask)
        {
            Task newTask = database.CreateTask(businessTask);
            database.LinkBusinessTaskToJob(businessTask.Id, Id);
            return newTask;
        }
    }
}
