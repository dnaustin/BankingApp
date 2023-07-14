namespace AccountsAPI.Models
{
    public class CurrentAccount : AccountBase
    {
        public CurrentAccount(int accountNumber, decimal balance) : base(accountNumber, balance)
        {
        }
    }
}
