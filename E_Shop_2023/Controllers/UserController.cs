using Core.Models;
using E_Shop_2023.Helpers;
using Infrastructure.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace E_Shop_2023.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly E_ShopContext _context;
        private readonly IConfiguration _config;
        private readonly IEmailService _emailService;

        public UserController(E_ShopContext context, IConfiguration configuration, IEmailService emailService)
        {
            _context = context;
            _config = configuration;
            _emailService = emailService;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] User userObj)
        {
            if (userObj is null)
                return BadRequest();
            
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Username == userObj.Username);

            if (user is null)
                return NotFound(new { Message = "User Not Found!" });

            if(!PasswordHasher.VerifyPassword(userObj.Password, user.Password))
                return BadRequest(new { Message = "Password is Incorrect" });

            // tạo token mới từ user
            user.Token = CreateJwt(user);

            var newAccessToken = user.Token;

            // tạo refresh token ( ngẫu nhiên )
            var newRefreshToken = CreateRefreshToken();
            user.RefreshToken = newRefreshToken; // gán vào user
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(5); // Thời hạn của RefreshToken
            await _context.SaveChangesAsync();
            
            return Ok(new TokenApiDTO
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            });
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User userObj)
        {
            if (userObj is null)
                return BadRequest();

            if (await CheckEmailExistAsync(userObj.Email))
                return BadRequest(new { Message = "Email Already Exist!" });

            if (await CheckUserNameExistAsync(userObj.Username))
                return BadRequest(new { Message = "Username Already Exist!"});

            var pass = CheckPasswordStrength(userObj.Password);
            if (!string.IsNullOrEmpty(pass))
                return BadRequest(new { Message = pass });


            userObj.Password = PasswordHasher.HashPassword(userObj.Password);
            //userObj.Role = "User";
            userObj.Token = "";
            await _context.Users.AddAsync(userObj);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                Message = "User Registerd!"
            });

        }

        private static string CheckPasswordStrength(string pass)
        {
            StringBuilder sb = new StringBuilder();
            if (pass.Length < 9)
                sb.Append("Minimum password length should be 8" + Environment.NewLine);
            if (!(Regex.IsMatch(pass, "[a-z]") && Regex.IsMatch(pass, "[A-Z]") && Regex.IsMatch(pass, "[0-9]")))
                sb.Append("Password should be AlphaNumeric or at least 1 Capital letter" + Environment.NewLine);
            if (!Regex.IsMatch(pass, "[<,>,@,!,#,$,%,^,&,*,(,),_,+,\\[,\\],{,},?,:,;,|,',\\,.,/,~,`,-,=]"))
                sb.Append("Password should contain special charcter" + Environment.NewLine);
            return sb.ToString();
        }

        private async Task<bool> CheckUserNameExistAsync(string userName)
            => await _context.Users.AnyAsync(x => x.Username == userName);
        private async Task<bool> CheckEmailExistAsync(string email)
            => await _context.Users.AnyAsync(x => x.Email == email);

        private string CreateJwt(User user)
        {
            //security token handler
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            // key -> dài 1 xíu
            var key = Encoding.ASCII.GetBytes("veryverysceret.....123123123123qweqweqweqwe123123123qweqweqweqwewqeqwe123123213");
            // payload
            var identity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            });

            // credentials param là byte ( trùng với key )
            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            // => dùng những cái ở trên để tạo ra Descriptor
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.Now.AddSeconds(10),
                SigningCredentials = credentials
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);

            return jwtTokenHandler.WriteToken(token);
            // token có 3 phần
            // header, payload, signature
        }

        private string CreateRefreshToken()
        {
            var tokenBytes = RandomNumberGenerator.GetBytes(64);

            var refreshToken = Convert.ToBase64String(tokenBytes);
            var tokenInUser = _context.Users
                .Any(a => a.RefreshToken == refreshToken);

            if(tokenInUser)
                return CreateRefreshToken();

            return refreshToken;
        }

        private ClaimsPrincipal GetPrincipleFromExpiredToken(string token)
        {
            // tạo key bytes
            var key = Encoding.ASCII.GetBytes("veryverysceret.....123123123123qweqweqweqwe123123123qweqweqweqwewqeqwe123123213");
            
            // chuẩn bị các tham số
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateLifetime = false,
            };
            // handler
            var tokenHandler = new JwtSecurityTokenHandler();
            // đầu ra
            SecurityToken securityToken;

            // 3 tham số, token, parameters, out -> security
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken is null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("This is Invalid Token");

            return principal;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<User>> GetAllUsers()
        {
            return Ok(await _context.Users.ToListAsync());
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(TokenApiDTO tokenApiDto)
        {
            if (tokenApiDto is null)
            {
                return BadRequest(new
                {
                    Message = "Invalid Client Request"
                });
            }
            string accessToken = tokenApiDto.AccessToken;
            string refreshToken = tokenApiDto.RefreshToken;
            // gọi hàm get Principal lấy thông tin của user đang đăng nhập (access token )
            var principal = GetPrincipleFromExpiredToken(accessToken);

            var username = principal.Identity.Name;

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

            // nếu user tồn tại, refreshtoken của user giống vs refreshtoken gửi về, token còn hạn sử dụng
            if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
                return BadRequest(new
                {
                    Message = "Invalid Request"
                });
            var newAccessToken = CreateJwt(user);
            var newRefreshToken = CreateRefreshToken();
            user.RefreshToken = newRefreshToken;
            await _context.SaveChangesAsync();
            return Ok(new TokenApiDTO
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
            });
        }


        /// <summary>
        /// Gửi email reset password
        /// Sinh ra 1 token reset password
        /// gửi email người dùng nhập, token trong URL
        /// </summary>
        /// <param name="email">Địa chỉ Email của người dùng</param>
        /// <returns></returns>
        [HttpPost("send-reset-email/{email}")]
        public async Task<IActionResult> SendEmail(string email)
        {
            // lấy ra user dựa trên email
            var user = await _context.Users.FirstOrDefaultAsync(a => a.Email == email);
            if (user is null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "email Doesn't Exist"
                });
            }
            // tạo ra refreshToken ( ngẫu nhiên )
            var tokenBytes = RandomNumberGenerator.GetBytes(64);
            var emailToken = Convert.ToBase64String(tokenBytes);
            //
            user.ResetPasswordToken = emailToken;
            user.ResetPasswordExpiry = DateTime.Now.AddMinutes(15);
            //string from = _config["EmailSettings:From"];
            
            // khởi tạo đối tượng EmailModel gồm (địa chỉ gửi đến, tiêu đề, nội dung)
            var emailModel = new EmailModel(email, "Reset Password !!", EmailBody.EmailStringBody(email, emailToken));
            // gửi mail
            _emailService.SendEmail(emailModel);
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(new
            {
                StatusCode = 200,
                Message = "Email Sent!"
            });
        }

        /// <summary>
        /// Lấy token từ user ở Db so sánh vs token ở tham số
        /// Nếu trùng thì hash password mới rồi gán vào user -> save
        /// </summary>
        /// <param name="resetPasswordDto">
        /// - email người dùng
        /// - token reset password
        /// - mật khẩu mới
        /// </param>
        /// <returns></returns>
        [HttpPost("rest-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO resetPasswordDto)
        {
            var newToken = resetPasswordDto.EmailToken.Replace(" ", "+");
            var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(a => a.Email == resetPasswordDto.Email);

            if (user is null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "email Doesn't Exist"
                });
            } 
            var tokenCode = user.ResetPasswordToken;
            DateTime? emailTokenExpiry = user.ResetPasswordExpiry;
            if (tokenCode != resetPasswordDto.EmailToken || emailTokenExpiry < DateTime.Now)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "Invalid Reset link"
                });
            }
            user.Password = PasswordHasher.HashPassword(resetPasswordDto.NewPassword);
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(new
            {
                StatusCode = 200,
                Message = "Password Reset Successfully"
            });
        }

        [HttpDelete("removeUser/{id}")]
        public async Task<IActionResult> RemoveUser(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(p => p.Id == id);
            if (user is null)
                return NotFound(new
                {
                    Message = "User not exist"
                });

            _context.Users.Remove(user);
            var result = _context.SaveChanges();

            if (result < 0)
                return BadRequest(new
                {
                    Message = "Xảy ra lỗi khi xóa !"
                });


            return Ok(new
            {
                Message = "Successfully"
            });
        }
    }
}
