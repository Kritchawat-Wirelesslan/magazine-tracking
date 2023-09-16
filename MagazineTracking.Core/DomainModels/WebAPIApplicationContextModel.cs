using MagazineTracking.Core.Externals.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagazineTracking.Core.DomainModels
{
	public class WebAPIApplicationContextModel : IApplicationContext
	{
		public string Username { get; set; }

		public string? Email { get; set; }

		public int? Role { get; set; }

		public string Token { get; set; }
		public static WebAPIApplicationContextModel ApplicationContextDTO(IApplicationContext applicationContext)
		{
			return new WebAPIApplicationContextModel()
			{
				Username = applicationContext.Username,
				Email = applicationContext.Email,
				Role = applicationContext.Role,
				Token = applicationContext.Token
			};
		}
	}
}
