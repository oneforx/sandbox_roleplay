namespace Roleplay.Bank
{
    public class BankAccount
    {
        public long Id;

        public long OwnerId;

        public BankAccount(long id, long ownerId)
        {
            this.Id = id;
            this.OwnerId = ownerId;
        }
    }
}