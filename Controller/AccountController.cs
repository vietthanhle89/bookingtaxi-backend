using bookingtaxi_backend.Model;
using bookingtaxi_backend.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bookingtaxi_backend.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;
        private readonly EmailService _emailServices;

        public AccountController(AccountService accountService, EmailService emailService)
        {
            _accountService = accountService;
            _emailServices = emailService;
        }

        //Administrator
        [Authorize]
        [RoleClaimRequires(new string[] { IdentityData.AdminAccountRoleClaimValue })]
        [HttpPost("Administrator")]
        public async Task<IActionResult> PostAdministrator(Administrator account)
        {
            var emailExists = await _accountService.IsEmailExisted(account.Email);
            

            if (emailExists == false)
            {
                Administrator createdAccount = await _accountService.CreateAdministrator(account);
                return CreatedAtAction("PostAdministrator", createdAccount);
            }

            return BadRequest(new ErrorResponse("Email is already existed"));
        }

        [Authorize]
        [RoleClaimRequires(new string[] { IdentityData.AdminAccountRoleClaimValue })]
        [HttpPut("Administrator")]
        public async Task<IActionResult> UpdateMemberAccount(Administrator account)
        {
            var obj = await _accountService.GetAccount(account.Id);

            if (obj is null)
            {
                return NotFound();
            }

            await _accountService.UpdateAdministrator(account);

            return Ok();
        }

        [Authorize]
        [RoleClaimRequires(new string[] { IdentityData.AdminAccountRoleClaimValue })]
        [HttpDelete("")]
        public async Task<IActionResult> DeleteAccount(String id)
        {
            var obj = await _accountService.GetAccount(id);

            if (obj is null)
            {
                return NotFound();
            }

            await _accountService.RemoveAccount(id);

            return Ok();
        }

        //Supporter
        [Authorize]
        [RoleClaimRequires(new string[] { IdentityData.AdminAccountRoleClaimValue })]
        [HttpPost("Supporter")]
        public async Task<IActionResult> PostSupporter(Supporter account)
        {
            var emailExists = await _accountService.IsEmailExisted(account.Email);


            if (emailExists == false)
            {
                Supporter createdAccount = await _accountService.CreateSupporter(account);
                return CreatedAtAction("PostSupporter", createdAccount);
            }

            return BadRequest(new ErrorResponse("Email is already existed"));
        }

        [Authorize]
        [RoleClaimRequires(new string[] { IdentityData.SupporterAccountRoleClaimValue })]
        [HttpPut("Supporter")]
        public async Task<IActionResult> UpdateSupporter(Supporter account)
        {
            var obj = await _accountService.GetAccount(account.Id);

            if (obj is null)
            {
                return NotFound();
            }

            await _accountService.UpdateSupporter(account);

            return Ok();
        }

        //Driver
        [HttpPost("Driver")]
        public async Task<IActionResult> PostDriver(Driver account)
        {
            var emailExists = await _accountService.IsEmailExisted(account.Email);


            if (emailExists == false)
            {
                Driver createdAccount = await _accountService.CreateDriver(account);
                return CreatedAtAction("PostDriver", createdAccount);
            }

            return BadRequest(new ErrorResponse("Email is already existed"));
        }

        [Authorize]
        [RoleClaimRequires(new string[] { IdentityData.DriverAccountRoleClaimValue })]
        [HttpPut("Driver")]
        public async Task<IActionResult> UpdateDriver(Driver account)
        {
            var obj = await _accountService.GetAccount(account.Id);

            if (obj is null)
            {
                return NotFound();
            }

            await _accountService.UpdateDriver(account);

            return Ok();
        }

        [Authorize]
        [RoleClaimRequires(new string[] { IdentityData.DriverAccountRoleClaimValue })]
        [HttpPost("ChangeDriverStatus")]
        public async Task<IActionResult> ChangeDriverStatus(string accountID, string driverStatusID)
        {
            var obj = await _accountService.GetAccount(accountID);

            if (obj is null)
            {
                return NotFound();
            }

            await _accountService.ChangeDriverStatus(accountID, driverStatusID);

            return Ok();
        }

        [Authorize]
        [RoleClaimRequires(new string[] { IdentityData.AdminAccountRoleClaimValue })]
        [HttpPost("ApproveDriver")]
        public async Task<IActionResult> ApproveDriver(string accountID)
        {
            var obj = await _accountService.GetAccount(accountID);

            if (obj is null)
            {
                return NotFound();
            }

            await _accountService.ApproveDriver(accountID);

            return Ok();
        }


        //Customer
        [HttpPost("Customer")]
        public async Task<IActionResult> PostCustomer(Customer account)
        {
            var emailExists = await _accountService.IsEmailExisted(account.Email);


            if (emailExists == false)
            {
                Customer createdAccount = await _accountService.CreateCustomer(account);
                return CreatedAtAction("PostCustomer", createdAccount);
            }

            return BadRequest(new ErrorResponse("Email is already existed"));
        }

        [Authorize]
        [RoleClaimRequires(new string[] { IdentityData.CustomerAccountRoleClaimValue })]
        [HttpPut("Customer")]
        public async Task<IActionResult> UpdateCustomer(Customer account)
        {
            var obj = await _accountService.GetAccount(account.Id);

            if (obj is null)
            {
                return NotFound();
            }

            await _accountService.UpdateCustomer(account);

            return Ok();
        }

    }
}
