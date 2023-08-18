using System.Collections.Generic;

namespace Roleplay.Business;

public interface IJobArchetype
{
    public string Name { get; set; }

    public bool IsGlobal { get; set; }

    public IList<JobTask> Tasks { get; set; }
}