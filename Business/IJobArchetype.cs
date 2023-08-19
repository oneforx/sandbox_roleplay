using System.Collections.Generic;

namespace Roleplay.Business;

public interface IJobArchetype
{
    public string Name { get; set; }

    public IList<JobTask> Tasks { get; set; }

    public IList<JobGrade> Grades { get; set; }
}

public class JobArchetype : IJobArchetype
{
    public string Name { get; set; }

    public IList<JobTask> Tasks { get; set; }

    public IList<JobGrade> Grades { get; set; }

    public JobArchetype(string name, IList<JobTask> tasks, IList<JobGrade> grades)
    {
        this.Name = name;
        this.Tasks = tasks;
        this.Grades = grades;
    }
}