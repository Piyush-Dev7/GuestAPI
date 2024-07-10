using DataAccess.Core.Auth.Contract;
using GuestSharedModel.DtoModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class AuthService: IAuthService
{
    private readonly string _secretKey = String.Empty; // Replace with your actual secret key
    private readonly string _issuer = String.Empty; // Replace with your actual issuer
    private readonly IConfigurationRoot _configurationManager;
    private readonly ApplicationDbContext _context;
    public AuthService(ApplicationDbContext context)
    {
        _configurationManager = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("data-access-appsettings.json", optional: false, reloadOnChange: true).Build();
        _secretKey = _configurationManager.GetRequiredSection("APISettings:SecretKey").Value ?? String.Empty;
        _issuer = _configurationManager.GetRequiredSection("APISettings:Issuer").Value ?? String.Empty;
        _context = context;
    }

    public string GenerateToken(UserDto user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_secretKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public async Task<Guid> ValidateCredentials(UserDto user)
    {
        var _user = (await _context.Users.FirstOrDefaultAsync(t => t.Email.Equals(user.Email) && t.IsActive));
        var _passHash = _user?.Password ?? String.Empty;
        if(string.IsNullOrWhiteSpace(_passHash))
            return Guid.Empty;

        bool verified = BCrypt.Net.BCrypt.Verify(user.Password, _passHash);
        return verified ? _user.Id : Guid.Empty;
    }

    public Guid ValidateToken(string token)
    {
        if (token == null)
            return Guid.Empty;

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_secretKey);
        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var userId = jwtToken.Claims.First(x => x.Type == "id").Value;
            Guid.TryParse(userId, out Guid _userId);
           
            return _userId;
        }
        catch
        {
            // return null if validation fails
            return Guid.Empty;
        }
    }
}
