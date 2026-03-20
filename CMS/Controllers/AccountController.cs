using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CMSAPI.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Persistence.Identity;

namespace CMSAPI.Controllers
{
    public class AccountController(SignInManager<User> signInManager,
                            RoleManager<IdentityRole> roleManager,
                            UserManager<User> userManager, IConfiguration _configuration) : BaseApiController
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
            // if (role.Name != "Client" && role.Name != "Lawyer")
            //     return BadRequest("Unauthorized user type selection");
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

        // [AllowAnonymous]
        // [HttpPost("login")]
        // public async Task<ActionResult> Login(LoginDto loginDto)
        // {
        //     var user = await signInManager.UserManager.FindByEmailAsync(loginDto.Email);
        //     if (user == null) return Unauthorized();

        //     if (!await signInManager.UserManager.CheckPasswordAsync(user, loginDto.Password))
        //         return Unauthorized();

        //     // if (useJwt)
        //     // {
        //     //     // Generate JWT token
        //     //     var token = GenerateJwtToken(user);
        //     //     return Ok(new { token });
        //     // }
        //     else
        //     {
        //         await signInManager.SignInAsync(user, false);
        //         return Ok(new
        //         {
        //             user.DisplayName,
        //             user.Email,
        //             user.Id
        //         });
        //     }
        // }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginDto loginDto)
        {
            var user = await userManager.FindByEmailAsync(loginDto.Email);
            if (user == null) return Unauthorized();

            if (!await userManager.CheckPasswordAsync(user, loginDto.Password))
                return Unauthorized();

            var token = await GenerateJwtToken(user);

            // If request is from web → set cookie
            if (loginDto.UseCookies)
            {
                Response.Cookies.Append("accessToken", token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.UtcNow.AddMinutes(60)
                });

                return Ok(new { user.DisplayName, user.Email });
            }

            // If request is from mobile → return token
            return Ok(new
            {
                token,
                user.DisplayName,
                user.Email
            });
        }


        // [AllowAnonymous]
        // [HttpGet("user-info")]
        // public async Task<ActionResult> GetUserInfo()
        // {
        //     if (User.Identity?.IsAuthenticated == false) return NoContent();

        //     var user = await signInManager.UserManager.GetUserAsync(User);

        //     if (user == null) return Unauthorized();

        //     return Ok(new
        //     {
        //         user.DisplayName,
        //         user.Email,
        //         user.Id,
        //         user.ImageUrl
        //     });
        // }

        [AllowAnonymous]
        [HttpGet("user-info")]
        public async Task<ActionResult> GetUserInfo()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var roles = await userManager.GetRolesAsync(user);

            return Ok(new
            {
                user.DisplayName,
                user.Email,
                user.Id,
                user.ImageUrl,
                Role = roles.FirstOrDefault(),
                UserType = roles.FirstOrDefault()
            });
        }

        // [HttpPost("logout")]
        // public async Task<ActionResult> Logout()
        // {
        //     await signInManager.SignOutAsync();

        //     return NoContent();
        // }
        [Authorize]
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("accessToken");

            return Ok(new { message = "Logged out successfully" });
        }
        private async Task<string> GenerateJwtToken(User user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");

            var roles = await userManager.GetRolesAsync(user);
            var claims = new List<Claim>
{
    new Claim(ClaimTypes.NameIdentifier, user.Id),
    new Claim(ClaimTypes.Email, user.Email!),
    new Claim("displayName", user.DisplayName ?? ""),
    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
};

            // Add role claims
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings["Key"]!)
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(
                    double.Parse(jwtSettings["DurationInMinutes"]!)
                ),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
