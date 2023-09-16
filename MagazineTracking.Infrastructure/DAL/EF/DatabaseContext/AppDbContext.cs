using MagazineTracking.Core.Externals.Repositories;
using MagazineTracking.Core.Models.Users;
using MagazineTracking.Infrastructure.DAL.EF.Mapings;
using Microsoft.EntityFrameworkCore;

namespace MagazineTracking.Infrastructure.EF.DatabaseContext
{
	public class AppDbContext : DbContext, IAppDBContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) 
		: base(options) 
		{ 

		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.MapUser();
			base.OnModelCreating(builder);
		}

		public DbSet<UserModel> Users => Set<UserModel>();

	}
}
