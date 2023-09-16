using MagazineTracking.Core.Externals.Repositories.Interface;

namespace MagazineTracking.WebAPI.Authetication
{
	public class WebAPIApplicationContext : IApplicationContext
	{
		public string Username { get; set; }

		public string? Email { get; set; }

		public int? Role { get; set; }
		public string Token { get; set; }
	}
}
