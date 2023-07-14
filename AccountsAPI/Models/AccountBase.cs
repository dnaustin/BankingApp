using AccountsAPI.Exceptions;

namespace AccountsAPI.Models
{
    public abstract class AccountBase : IAccount
    {
        public AccountBase(int accountNumber, decimal balance)
        {
            AccountNumber = accountNumber;
            Balance = balance;
        }

        public int AccountNumber { get; }

        public decimal Balance { get; private set; }

        public bool Frozen { get; private set; } = false;

        public void Credit(decimal amount)
        {
            if (Frozen)
            {
                throw new FrozenAccountException();
            }

            Balance += amount;
        }

        public void Debit(decimal amount)
        {
            if (Frozen)
            {
                throw new FrozenAccountException();
            }

            Balance -= amount;
        }

        public void Freeze()
        {
            Frozen = true;
        }
    }
}
