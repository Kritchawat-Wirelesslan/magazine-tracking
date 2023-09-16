using MagazineTracking.Core.Models.Users;

namespace MagazineTracking.Core.Models.Authentications
{
	public class AuthenticateResponse
	{
		public Guid UserId { get; set; }
		public string Username { get; set; }
		public string Email { get; set; }
		public string Token { get; set; }
		public DateTime Expire { get; set; }



		public AuthenticateResponse(UserModelDTO user, string token , DateTime dateTime)
		{
			UserId = (Guid)user.UserId;
			Username = user.Username;
			Email = user.Email;
			Token = token;
			Expire = dateTime.ToUniversalTime();
		}

	}
}
