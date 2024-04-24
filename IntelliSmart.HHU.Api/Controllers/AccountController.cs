using IntelliSmart.HHU.Api.Models.Account;
using IntelliSmart.HHU.Api.Models.Domain;
using IntelliSmart.HHU.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IntelliSmart.HHU.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public IAccountService _accountService { get; }
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        ApiResponse response = new ApiResponse();



        [HttpPost("ValidateUser")]
        public IActionResult ValidateUser(Login login)
        {
            try
            {
                var result = _accountService.ValidateUser(login);
                if (result != null)
                {
                    response.StatusCode = 200;
                    response.Message = "Login Successful!";
                    response.Data = result;
                    return Ok(response);
                }
                else
                {
                    response.StatusCode = 100;
                    response.Message = "User not found!";
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Message = "Something went wrong";
                response.Data = ex.Message;
                return StatusCode(500, response);
            }
        }


        [HttpPost("RegisterUser")]
        public IActionResult RegisterUser(Register register)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    response.StatusCode = 100;
                    response.Message = "Invalid model state";
                    response.Data = ModelState;
                    return BadRequest(response);
                }

                if (_accountService.RegisterUser(register))
                {
                    response.StatusCode = 200;
                    response.Message = "Registration Successful!";
                    return Ok(response);
                }
                else
                {
                    response.StatusCode = 100;
                    response.Message = "Failed to register user";
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == "Email already exists")
                {
                    response.StatusCode = 100;
                    response.Message = ex.Message;
                    return BadRequest(response);
                }
                else
                {
                    response.StatusCode = 500;
                    response.Message = "Something went wrong";
                    response.Data = ex.Message;
                    return StatusCode(500, response);
                }
            }
        }



        [HttpPost("UpdateUser")]
        public IActionResult UpdateUser(Register register)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    response.StatusCode = 100;
                    response.Message = "Invalid model state";
                    response.Data = ModelState;
                    return BadRequest(response);
                }

                if (_accountService.UpdateUser(register))
                {
                    response.StatusCode = 200;
                    response.Message = "Update Successful!";
                    return Ok(response);
                }
                else
                {
                    response.StatusCode = 100;
                    response.Message = "Failed to update user";
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Message = "Something went wrong";
                response.Data = ex.Message;
                return StatusCode(500, response);
            }
        }


        [HttpDelete("DeleteUser")]
        public IActionResult DeleteUser(int userId)
        {
            try
            {
                if (_accountService.DeleteUser(userId))
                {
                    response.StatusCode = 200;
                    response.Message = "User deleted successfully!";
                    return Ok(response);
                }
                else
                {
                    response.StatusCode = 100;
                    response.Message = "User not found or failed to delete";
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Message = "Something went wrong";
                response.Data = ex.Message;
                return StatusCode(500, response);
            }
        }

    }
}