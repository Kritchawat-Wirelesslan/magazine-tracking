using System.ComponentModel.DataAnnotations;
namespace MagazineTracking.Infrastructure
{
	public class AuthenticateRequest
	{
		[Required]
		public string Username { get; set; }
		[Required]
		public string Password { get; set; }
	}
}
