using AccountsAPI.Dtos;
using AutoMapper;
using System;

namespace AccountsAPI.Models
{
    public static class AccountFactory
    {
        public static Account GetAccount(CreateAccountDto accountDto, IMapper mapper)
        {
            if (accountDto == null)
            {
                throw new ArgumentNullException(nameof(accountDto));
            }

            switch (accountDto.Type.ToLowerInvariant())
            {
                case "current":
                    return mapper.Map<CurrentAccount>(accountDto);
                case "savings":
                    return mapper.Map<SavingsAccount>(accountDto);
                default:
                    throw new ArgumentOutOfRangeException(nameof(accountDto));
            }
        }
    }
}
