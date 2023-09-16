

using MagazineTracking.Core.Externals.Repositories.Interface;
using MagazineTracking.Core.Models.Users;

namespace MagazineTracking.Infrastructure.Externals.Repositories.Interface.User
{
	public interface IUserRepository
	{
		Task<UserModel> LoginByUser(string username, string password);
		Task<List<UserModelDTO>> GetUserList();
		IApplicationContext GetUserContext();
	}
}
