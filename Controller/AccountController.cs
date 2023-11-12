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
        public async Task<IActionResult> UpdateAdministratorAccount(Administrator account)
        {
            var obj = await _accountService.GetAdministrator(account.Id);

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
            var obj = await _accountService.GetAdministrator(id);

            if (obj is null)
            {
                return NotFound();
            }

            await _accountService.RemoveAccount(id);

            return Ok();
        }

        [Authorize]
        [RoleClaimRequires(new string[] { IdentityData.AdminAccountRoleClaimValue })]
        [HttpGet("GetAllAdministrators")]
        public async Task<List<Account>> GetAllAdministrators()
        {
            return await _accountService.GetAllAdministrators();
        }

        [Authorize]
        [RoleClaimRequires(new string[] { IdentityData.AdminAccountRoleClaimValue })]
        [HttpGet("GetAdministrator")]
        public async Task<Account?> GetAdministrator(string id)
        {
            return await _accountService.GetAdministrator(id);
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
            var obj = await _accountService.GetSupporter(account.Id);

            if (obj is null)
            {
                return NotFound();
            }

            await _accountService.UpdateSupporter(account);

            return Ok();
        }

        [Authorize]
        [RoleClaimRequires(new string[] { IdentityData.AdminAccountRoleClaimValue })]
        [HttpGet("GetAllSupporters")]
        public async Task<List<Account>> GetAllSupporters()
        {
            return await _accountService.GetAllSupporters();
        }

        [Authorize]
        [RoleClaimRequires(new string[] { IdentityData.AdminAccountRoleClaimValue })]
        [HttpGet("GetSupporter")]
        public async Task<Account?> GetSupporter(string id)
        {
            return await _accountService.GetSupporter(id);
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
            var obj = await _accountService.GetDriver(account.Id);

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
            var obj = await _accountService.GetDriver(accountID);

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
            var obj = await _accountService.GetDriver(accountID);

            if (obj is null)
            {
                return NotFound();
            }

            await _accountService.ApproveDriver(accountID);

            return Ok();
        }

        [Authorize]
        [RoleClaimRequires(new string[] { IdentityData.AdminAccountRoleClaimValue })]
        [HttpGet("GetAllDrivers")]
        public async Task<List<Driver>> GetAllDrivers()
        {
            return await _accountService.GetAllDrivers();
        }

        [Authorize]
        [RoleClaimRequires(new string[] { IdentityData.AdminAccountRoleClaimValue, IdentityData.DriverAccountRoleClaimValue })]
        [HttpGet("GetDriver")]
        public async Task<Driver?> GetDriver(string id)
        {
            return await _accountService.GetDriver(id);
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
            var obj = await _accountService.GetCustomer(account.Id);

            if (obj is null)
            {
                return NotFound();
            }

            await _accountService.UpdateCustomer(account);

            return Ok();
        }

        [Authorize]
        [RoleClaimRequires(new string[] { IdentityData.AdminAccountRoleClaimValue })]
        [HttpGet("GetAllCustomers")]
        public async Task<List<Customer>> GetAllCustomers()
        {
            return await _accountService.GetAllCustomers();
        }

        [Authorize]
        [RoleClaimRequires(new string[] { IdentityData.AdminAccountRoleClaimValue, IdentityData.CustomerAccountRoleClaimValue })]
        [HttpGet("GetCustomer")]
        public async Task<Customer?> GetCustomer(string id)
        {
            return await _accountService.GetCustomer(id);
        }


        [Authorize]
        [HttpPost("Role")]
        public async Task<IActionResult> PostRole(Role obj)
        {
            Role createdObj = await _accountService.CreateCarType(obj);
            return CreatedAtAction("PostRole", createdObj);
        }

        [Authorize]
        [HttpDelete("Role")]
        public async Task<IActionResult> DeleteCarType(String id)
        {
            await _accountService.DeleteRole(id);
            return Ok();
        }

        [Authorize]
        [HttpGet("GetAllRoles")]
        public async Task<List<Role>> GetAllRoles()
        {
            return await _accountService.GetAllRoles();
        }

        [Authorize]
        [HttpGet("Role")]
        public async Task<Role?> GetRolee(string id)
        {
            return await _accountService.GetRole(id);
        }
    }
}
