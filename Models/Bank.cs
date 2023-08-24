using System.Collections.Generic;

namespace Roleplay.Models
{
    public class Bank : Roleplay.Systems.Table
    {
        public string Title;

        public List<BankAccount> Accounts;
        public Bank(string title)
        {
            Title = title;
            Accounts = new();
        }
    }
}