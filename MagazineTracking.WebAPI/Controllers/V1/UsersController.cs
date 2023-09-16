using MagazineTracking.Infrastructure.Externals.Repositories.Interface.User;
using MagazineTracking.WebAPI.AuthorizeAttribute;
using Microsoft.AspNetCore.Mvc;

namespace MagazineTracking.WebAPI.Controllers.V1
{
    [ApiController]
    [Route("api/v1/users")]
    public class UsersController : ControllerBase
	{
        private IUserRepository _userRepository;
        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;

		}
        [MagazineAuthorization]
        [HttpGet("")]
		public async Task<IActionResult> GetUsers()
		{
            var user =  await _userRepository.GetUserList();
            return Ok(user);
		}
		[MagazineAuthorization]
		[HttpGet("me")]
		public async Task<IActionResult> CurrentUser()
		{
			var result = this._userRepository.GetUserContext();
			return Ok(result);
		}
	}
}
