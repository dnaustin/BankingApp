using AccountsAPI.Dtos;
using AccountsAPI.Models;
using AccountsAPI.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AccountsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountRepository accountRepository;
        private readonly IMapper mapper;

        public AccountsController(IAccountRepository accountRepository, IMapper mapper)
        {
            this.accountRepository = accountRepository;
            this.mapper = mapper;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateAccountDto accountDto)
        {
            try
            {
                Account accountToCreate = AccountFactory.GetAccount(accountDto, mapper);
                accountRepository.CreateAccount(accountToCreate);
                ReturnAccountDto createdAccountDto = mapper.Map<ReturnAccountDto>(accountToCreate);

                return Ok(createdAccountDto);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("{accountNumber}")]
        public IActionResult Update(int accountNumber, [FromBody] UpdateAccountDto updateAccountDto)
        {
            try
            {
                Account accountToUpdate = accountRepository.GetAccount(accountNumber);

                if (accountToUpdate is null)
                {
                    return NotFound();
                }

                accountToUpdate.UpdateName(updateAccountDto.Name);
                accountRepository.UpdateAccount(accountToUpdate);
                ReturnAccountDto updatedAccountDto = mapper.Map<ReturnAccountDto>(accountToUpdate);
                return Ok(updatedAccountDto);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("{accountNumber}")]
        public IActionResult Delete(int accountNumber)
        {
            try
            {
                Account deletedAccount = accountRepository.DeleteAccount(accountNumber);
                return Ok();
            }
            catch
            {
                return Conflict(); // probably a better return result but will do for this demo
            }
        }

        [HttpPut("{accountNumber}/freeze")]
        public IActionResult Freeze(int accountNumber)
        {
            try
            {
                Account accountToFreeze = accountRepository.GetAccount(accountNumber);

                if (accountToFreeze is null)
                {
                    return NotFound();
                }

                accountToFreeze.Freeze();
                accountRepository.UpdateAccount(accountToFreeze);
                ReturnAccountDto updatedAccountDto = mapper.Map<ReturnAccountDto>(accountToFreeze);
                return Ok(updatedAccountDto);
            }
            catch
            {
                return BadRequest();
            }
        }

    }
}
