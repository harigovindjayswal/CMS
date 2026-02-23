using CMSAPI.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Persistence.Identity;

namespace CMSAPI.Controllers
{
    public class AccountController(SignInManager<User> signInManager,
                            RoleManager<IdentityRole> roleManager,
                            UserManager<User> userManager) : BaseApiController
    {
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser(RegisterDto registerDto)
        {
            if (string.IsNullOrWhiteSpace(registerDto.UserType))
                return BadRequest("User type is required");
            var role = await roleManager.FindByIdAsync(registerDto.UserType);
            if (role == null)
                return BadRequest("Invalid user type selected");
            // Optional: restrict allowed roles (VERY IMPORTANT for security)
            if (role.Name != "Client" && role.Name != "Lawyer")
                return BadRequest("Unauthorized user type selection");
            var user = new User
            {
                UserName = registerDto.Email,
                Email = registerDto.Email,
                DisplayName = registerDto.DisplayName
            };
            var result = await signInManager.UserManager.CreateAsync(user, registerDto.Password);
            if (result.Succeeded)
            {
                var roleResult = await userManager.AddToRoleAsync(user, role.Name);
                if (!roleResult.Succeeded)
                {
                    // rollback user if role assignment fails
                    await userManager.DeleteAsync(user);
                    return BadRequest("Failed to assign user Type");

                }
                return Ok();
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }
            return ValidationProblem();
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginDto loginDto)
        {
            var user = await signInManager.UserManager.FindByEmailAsync(loginDto.Email);
            if (user == null) return Unauthorized();

            if (!await signInManager.UserManager.CheckPasswordAsync(user, loginDto.Password))
                return Unauthorized();

            // if (useJwt)
            // {
            //     // Generate JWT token
            //     var token = GenerateJwtToken(user);
            //     return Ok(new { token });
            // }
            else
            {
                await signInManager.SignInAsync(user, false);
                return Ok(new
                {
                    user.DisplayName,
                    user.Email,
                    user.Id
                });
            }
        }


        [AllowAnonymous]
        [HttpGet("user-info")]
        public async Task<ActionResult> GetUserInfo()
        {
            if (User.Identity?.IsAuthenticated == false) return NoContent();

            var user = await signInManager.UserManager.GetUserAsync(User);

            if (user == null) return Unauthorized();

            return Ok(new
            {
                user.DisplayName,
                user.Email,
                user.Id,
                user.ImageUrl
            });
        }

        [HttpPost("logout")]
        public async Task<ActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return NoContent();
        }
    }
}
