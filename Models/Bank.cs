using System.Collections.Generic;

namespace Roleplay.Models
{
    public class Bank
    {
        public long Id;

        public string Title;

        public List<BankAccount> Accounts;
        public Bank(long id, string title)
        {
            Id = id;
            Title = title;
            Accounts = new();
        }
    }
}