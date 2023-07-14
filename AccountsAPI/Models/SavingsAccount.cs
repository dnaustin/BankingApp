namespace AccountsAPI.Models
{
    public class SavingsAccount : AccountBase
    {
        public SavingsAccount(int accountNumber, decimal balance) : base(accountNumber, balance)
        {
        }
    }
}
