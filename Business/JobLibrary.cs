using System.Collections.Generic;

namespace Roleplay.Business
{
    public static partial class JobLibrary
    {
        public static JobGrade Chief = new(0, "Chief", 2);
        public static JobGrade Manager = new(1, "Manager", 1);
        public static JobGrade Novice = new(2, "Novice", 0);

        public static Job ButcherJob = new("Butcher", new List<JobGrade>
        {
            Chief,
            Manager,
            Novice
        }, PaymentMethod.TaskCompletion);

        public static Job PoliceJob = new("Police", new List<JobGrade>
        {
            Chief,
            Manager,
            Novice
        }, PaymentMethod.RegularInterval);
    }
}