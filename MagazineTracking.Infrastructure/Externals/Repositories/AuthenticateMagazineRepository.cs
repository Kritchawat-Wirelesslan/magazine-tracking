using MagazineTracking.Core.Models.Authentications;
using MagazineTracking.Core.Models.Users;
using MagazineTracking.Infrastructure.Externals.Repositories.Interface.Authenticate;
using MagazineTracking.Infrastructure.Externals.Repositories.Interface.User;
using MagazineTracking.WebAPI.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MagazineTracking.Infrastructure.Externals.Repositories
{
	public class AuthenticateMagazineRepository : IAuthenticateMagazineRepository
	{
		private readonly AppSettings _appSettings;
		private IUserRepository _userRepository;

		public AuthenticateMagazineRepository(IOptions<AppSettings> _appSetting, IUserRepository userRepository)
        {
			_appSettings = _appSetting.Value;
			_userRepository = userRepository;
		}

        public async Task<AuthenticateResponse> Authentication(AuthenticateRequest model)
		{
			UserModelDTO _userModelDTOs = new UserModelDTO();

			var user = await _userRepository.LoginByUser(model.Username, model.Password);
			_userModelDTOs.Username = user.Username;
			_userModelDTOs.UserId = user.UserId;
			_userModelDTOs.Role = user.Role;
			_userModelDTOs.Email = user.Email;

			// return null if user not found
			if (user == null) return null;
			// authentication successful so generate jwt token
			var token = generateJwtToken(user);

			return new AuthenticateResponse(_userModelDTOs, token, DateTime.UtcNow.AddDays(7));
		}

		// helper methods
		private string generateJwtToken(UserModel user)
		{
			// generate token that is valid for 7 days
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
			var expireDate = DateTime.UtcNow.AddDays(7).ToString();

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new[]{
				new Claim("Username",user.Username),
				new Claim("Password",user.Password),
				new Claim("Role",user.Role.ToString()),
				new Claim("Email",user.Email),
				new Claim("Expire",expireDate),

				}),
				Expires = DateTime.UtcNow.AddDays(7),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}
	}
}
