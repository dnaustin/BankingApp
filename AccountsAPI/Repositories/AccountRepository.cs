using AccountsAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace AccountsAPI.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApiContext context;

        public AccountRepository(ApiContext context)
        {
            this.context = context;
        }

        public Account CreateAccount(Account account)
        {
            if (account is null)
            {
                throw new ArgumentNullException(nameof(account));
            }
            
            try
            {
                context.Accounts.Add(account);
                context.SaveChanges();
                return account; 
            }
            catch
            {
                return null;
            }
        }

        public Account DeleteAccount(int accountNumber)
        {
            try
            {
                Account accountToDelete = context.Accounts.Find(accountNumber);
                    
                // no need to throw if null as the record doesn't exist so is essentially deleted
                if (accountToDelete is not null)
                {
                    context.Accounts.Remove(accountToDelete);
                    context.SaveChanges();
                }
                    
                return accountToDelete;
            }
            catch
            {
                return null;
            }
        }

        public Account GetAccount(int accountNumber)
        {
            return context.Accounts.Find(accountNumber);
        }

        public Account UpdateAccount(Account account)
        {
            if (account is null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            try
            {
                context.Entry(account).State = EntityState.Modified;
                context.SaveChanges();

                return account;
            }
            catch
            {
                return null;
            }
        }
    }
}
