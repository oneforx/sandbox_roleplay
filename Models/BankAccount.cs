namespace Roleplay.Models
{
    public class BankAccount : Roleplay.Systems.Table
    {
        public float Money { get; set; }

        public BankAccount(float money) : base("bankAccount")
        {
            Money = money;
        }
    
        public void AddMoney(float money)
        {
            this.Money += money;
        }

        public void SetMoney(float money)
        {
            this.Money = money;
        }

        public void TransferMoney(float money, BankAccount account) 
        {
            if (money < 0 && this.Money - money < 0) return;
            
            account.Money = money;
        }
    }
}