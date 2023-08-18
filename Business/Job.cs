using System;
using System.Collections.Generic;

namespace Roleplay.Business
{
    public enum PaymentMethod
    {
        RegularInterval,    // Paiement à intervalles réguliers
        TaskCompletion     // Paiement après avoir réalisé toutes les tâches demandées
    }


    public class Job {
        public Guid Id = Guid.NewGuid();
        public string Title;
        public List<JobGrade> Grades;
        public PaymentMethod PaymentType;

        public Job(string title, List<JobGrade> grades, PaymentMethod paymentType)
        {
            this.Title = title;
            this.Grades = grades;
            this.PaymentType = paymentType;

        }
    }

    public class JobGrade {
        public long Id;
        public int Priority;
        public string Title;

        public JobGrade(long id, string title, int priority)
        {
            this.Id = id;
            this.Title = title;
            this.Priority = priority;
        }
    }
}