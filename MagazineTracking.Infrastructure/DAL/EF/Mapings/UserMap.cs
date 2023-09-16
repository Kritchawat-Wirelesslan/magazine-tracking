using MagazineTracking.Core.Models.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagazineTracking.Infrastructure.DAL.EF.Mapings
{
	public static class UserMap
	{
		public static ModelBuilder MapUser (this ModelBuilder modelBuilder)
		{
			var entiry = modelBuilder.Entity<UserModel>();
			entiry.Property(x => x.UserId).ValueGeneratedOnAdd();
			entiry.Property(x => x.Username);
			entiry.Property(x => x.Email);
			entiry.Property(x => x.Password);
			entiry.Property(x => x.Role);
			entiry.HasKey(x => x.UserId);
			entiry.ToTable("Users");

			return modelBuilder;
		}
	}
}
