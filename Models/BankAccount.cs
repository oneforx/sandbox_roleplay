namespace Roleplay.Models
{
    public class BankAccount
    {
        public long Id;

        public long OwnerId;

        public BankAccount(long id, long ownerId)
        {
            Id = id;
            OwnerId = ownerId;
        }
    }
}