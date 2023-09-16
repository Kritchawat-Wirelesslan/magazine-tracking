using MagazineTracking.Core.DomainModels;
using MagazineTracking.Core.Externals.Repositories.Interface;
using MagazineTracking.Core.Models.Users;
using MagazineTracking.Infrastructure.EF.DatabaseContext;
using MagazineTracking.Infrastructure.Externals.Repositories.Interface.User;

namespace MagazineTracking.Infrastructure.Externals.Repositories
{

	public class UserRepository : IUserRepository
	{
		private readonly AppDbContext _context;
		private IApplicationContext applicationContext;

		public UserRepository(AppDbContext context, IApplicationContext application)
		{
			_context = context;
			applicationContext = application;

		}
		public async Task<UserModel> LoginByUser(string username, string password)
		{
			var userConext = this._context.Users.Where(x => x.Username == username && x.Password == password).FirstOrDefault();
			return userConext;
		}

		public async Task<List<UserModelDTO>> GetUserList()
		{
			var userConext = this._context.Users.ToList();
			UserModelDTO userModelDTO = new UserModelDTO();
			List<UserModelDTO> list = new List<UserModelDTO>();
			foreach (var user in userConext)
			{
				userModelDTO.UserId = user.UserId;
				userModelDTO.Username = user.Username;
				userModelDTO.Role = user.Role;
				userModelDTO.Email = user.Email;
				list.Add(userModelDTO);
			}

			return list;
		}
		public IApplicationContext GetUserContext()
		{
			var contextResult = WebAPIApplicationContextModel.ApplicationContextDTO(applicationContext);
			return contextResult;
		}
	}
}
