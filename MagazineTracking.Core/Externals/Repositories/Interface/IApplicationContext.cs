using MagazineTracking.Core.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagazineTracking.Core.Externals.Repositories.Interface
{
	public interface IApplicationContext
	{
		public string Username { get; set; }

		public string? Email { get; set; }

		public int? Role { get; set; }
		public string Token { get; set; }
	}
}
