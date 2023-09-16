using MagazineTracking.Core.Models.Authentications;

namespace MagazineTracking.Infrastructure.Externals.Repositories.Interface.Authenticate
{
	public interface IAuthenticateMagazineRepository
	{
		Task<AuthenticateResponse> Authentication(AuthenticateRequest model);
	}
}
