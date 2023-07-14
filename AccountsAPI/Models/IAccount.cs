namespace AccountsAPI.Models
{
    public interface IAccount
    {
        int AccountNumber { get; }
        decimal Balance { get; }
        bool Frozen { get; }

        void Credit(decimal amount);
        void Debit(decimal amount);
        void Freeze();
    }
}
