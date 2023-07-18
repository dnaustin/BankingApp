using AccountsAPI.Models;

namespace AccountsAPI.Repositories
{
    public interface IAccountRepository
    {
        public Account CreateAccount(Account account);
        public Account GetAccount(int accountNumber);
        public Account UpdateAccount(Account account);
        public Account DeleteAccount(int accountNumber);
    }
}
