using MagazineTracking.Infrastructure;
using MagazineTracking.Infrastructure.Externals.Repositories.Interface.Authenticate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MagazineTracking.WebAPI.Controllers.V1
{
    [ApiController]
	[Route("api/v1/authentications")]
	public class AuthenticationsController : ControllerBase
	{
		private IAuthenticateMagazineRepository _authenticateMagazineRepository;
		public AuthenticationsController(IAuthenticateMagazineRepository _authenticateMagazine)
		{
			this._authenticateMagazineRepository = _authenticateMagazine;
		}
		[AllowAnonymous]
		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] AuthenticateRequest command)
		{
			var response = await _authenticateMagazineRepository.Authentication(command);

			if (response == null)
				return BadRequest(new { message = "Username or password is incorrect" });

			return Ok(response);
		}

	}
}
