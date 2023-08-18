using System.Collections.Generic;

namespace Roleplay.Bank
{
    public class Bank
    {
        public long Id;

        public string Title;

        public List<BankAccount> Accounts;
        public Bank(long id, string title)
        {
            this.Id = id;
            this.Title = title;
            this.Accounts = new();
        }
    }
}