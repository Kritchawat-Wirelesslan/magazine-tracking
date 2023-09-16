using MagazineTracking.Core.DomainModels.Base;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagazineTracking.Core.Models.Users
{
	public class UserModel : EntityBase<Guid>
	{
		public Guid UserId { get; set; }

		public string Username { get; set; }

		public string Password { get; set; }

		public string Email { get; set; }

		public int? Role { get; set; } = (int)Roles.Restrict;
		protected UserModel()
		{
		}
	}

	public class UserModelDTO
	{
		public Guid? UserId { get; set; }
		public string Username { get; set; }
		
		public string? Email { get; set; }
	
		public int? Role { get; set; } = (int)Roles.Restrict;
	}

	

	enum  Roles
	{
		Restrict,
		Administrator,
		User,
	}

}
