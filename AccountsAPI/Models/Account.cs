using AccountsAPI.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace AccountsAPI.Models
{
    public class Account
    {
        [Key]
        public int AccountNumber { get; set; }
        public decimal Balance { get; private set; }
        public bool Frozen { get; private set; }
        public string Name { get; private set; }
        public string Type { get; set; }

        public void UpdateName(string name)
        {
            Name = name;
        }

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
