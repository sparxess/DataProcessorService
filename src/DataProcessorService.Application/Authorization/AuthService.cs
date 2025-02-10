using DataProcessorService.Application.Contracts.Authorization;
using DataProcessorService.Domain.Authorization;
using DataProcessorService.Domain.Shared.Authorization;
using DataProcessorService.Domain.Shared.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DataProcessorService.Application.Authorization;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    IOptions<JwtSettings> _jwtSettings;

    public AuthService(UserManager<User> userManager,
        SignInManager<User> signInManager,
        IOptions<JwtSettings> jwtSettings)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtSettings = jwtSettings;
    }

    public async Task<string> RegisterAsync(string username, string password)
    {
        var user = new User { UserName = username };
        var result = await _userManager.CreateAsync(user, password);

        if (result.Succeeded)
        {
            return AuthMessages.UserRegisteredSuccessfully;
        }

        return AuthMessages.RegistrationFailed;
    }

    public async Task<AuthResult> LoginAsync(string username, string password)
    {
        var user = await _userManager.FindByNameAsync(username);
        if (user == null)
        {
            return new AuthResult { Success = false, ErrorMessage = AuthMessages.UserNotFound };
        }

        var result = await _signInManager.PasswordSignInAsync(user, password, false, false);
        if (!result.Succeeded)
        {
            return new AuthResult { Success = false, ErrorMessage = AuthMessages.LoginFailed };
        }

        var token = GenerateJwtToken(user);

        return new AuthResult { Success = true, Token = token };
    }

    private string GenerateJwtToken(User user)
    {
        var claims = GetClaims(user);
        var token = CreateJwtToken(claims);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private IEnumerable<Claim> GetClaims(User user)
    {
        return new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
    }

    private JwtSecurityToken CreateJwtToken(IEnumerable<Claim> claims)
    {

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Value.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        return new JwtSecurityToken(
            _jwtSettings.Value.Issuer,
            _jwtSettings.Value.Audience,
            claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds
        );
    }
}