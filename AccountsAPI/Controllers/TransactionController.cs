using AccountsAPI.Dtos;
using AccountsAPI.Exceptions;
using AccountsAPI.Models;
using AccountsAPI.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AccountsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IAccountRepository accountRepository;
        private readonly IMapper mapper;

        public TransactionController(IAccountRepository accountRepository, IMapper mapper)
        {
            this.accountRepository = accountRepository;
            this.mapper = mapper;
        }

        [HttpPut]
        public IActionResult Update([FromBody] TransactionDto transactionDto)
        {
            switch (transactionDto.Type.ToLower())
            {
                case "debit":
                    return Debit(transactionDto.AccountNumberToDebit, transactionDto.Amount);
                case "credit":
                    return Credit(transactionDto.AccountNumberToCredit, transactionDto.Amount);
                case "transfer":
                    return Transfer(transactionDto);
                default:
                    return BadRequest();
            }
        }

        private IActionResult Transfer(TransactionDto transactionDto)
        {
            // Debit account 1
            IActionResult debitResult = Debit(transactionDto.AccountNumberToDebit, transactionDto.Amount);

            if (debitResult is not OkResult)
            {
                return debitResult;
            }

            // Credit account 2
            IActionResult creditResult = Credit(transactionDto.AccountNumberToCredit, transactionDto.Amount);

            if (creditResult is OkResult)
            {
                return creditResult;
            }

            // Credit failed so revert debit by crediting the first account
            Credit(transactionDto.AccountNumberToDebit, transactionDto.Amount);
            return creditResult;
        }

        private IActionResult Credit(int? accountNumberToCredit, decimal amountToCredit)
        {
            if (accountNumberToCredit is null || accountNumberToCredit == 0)
            {
                return BadRequest();
            }

            if (amountToCredit <= 0)
            {
                return BadRequest();
            }

            Account accountToCredit = accountRepository.GetAccount(accountNumberToCredit ?? 0);

            if (accountToCredit is null)
            {
                return NotFound();
            }

            try
            {
                accountToCredit.Credit(amountToCredit);
            }
            catch (FrozenAccountException)
            {
                return BadRequest(accountToCredit);
            }
            
            accountRepository.UpdateAccount(accountToCredit);
            ReturnAccountDto creditedAccountDto = mapper.Map<ReturnAccountDto>(accountToCredit);

            return Ok(creditedAccountDto);
        }

        private IActionResult Debit(int? accountNumberToDebit, decimal amountToDebit)
        {
            if (accountNumberToDebit is null || accountNumberToDebit == 0)
            {
                return BadRequest();
            }

            if (amountToDebit <= 0)
            {
                return BadRequest();
            }

            Account accountToDebit = accountRepository.GetAccount(accountNumberToDebit ?? 0);

            if (accountToDebit is null)
            {
                return NotFound();
            }

            try
            {
                accountToDebit.Debit(amountToDebit);
            }
            catch (FrozenAccountException)
            {
                return BadRequest(accountToDebit);
            }

            accountRepository.UpdateAccount(accountToDebit);
            ReturnAccountDto debitedAccountDto = mapper.Map<ReturnAccountDto>(accountToDebit);

            return Ok(debitedAccountDto);
        }

    }
}
