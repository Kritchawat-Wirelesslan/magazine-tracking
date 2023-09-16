using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using MagazineTracking.Core.Externals.Repositories.Interface;
using MagazineTracking.Infrastructure.EF.DatabaseContext;
using MagazineTracking.Core.Models.Users;

namespace MagazineTracking.WebAPI.AuthorizeAttribute
{
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
	public class MagazineAuthorizationAttribute : Attribute, IAuthorizationFilter
	{
		private IConfiguration configuration;
		public MagazineAuthorizationAttribute()
		{
		}
		public void OnAuthorization(AuthorizationFilterContext context)
		{
			IServiceProvider serviceProvider = context.HttpContext.RequestServices;
			var idtoken = context.HttpContext.Request.Headers.Authorization.ToString();
			IApplicationContext? applicationContext = serviceProvider.GetService(typeof(IApplicationContext)) as IApplicationContext;
			AppDbContext _dbContext = serviceProvider.GetService(typeof(AppDbContext)) as AppDbContext;
			
			if (idtoken != "") 
			{
				string tokenKey = idtoken.Replace("Bearer ", "");
				if (!string.IsNullOrEmpty(tokenKey))
				{
					var splitToKen = idtoken.Split(" ")[1];
					var token = new JwtSecurityToken(jwtEncodedString: splitToKen);
					applicationContext.Username = token.Claims.First(claim => claim.Type == "Username").Value;
					applicationContext.Email = token.Claims.First(claim => claim.Type == "Email").Value;
					applicationContext.Role = int.Parse(token.Claims.First(claim => claim.Type == "Role").Value);
					applicationContext.Token = tokenKey;
					var password = token.Claims.First(claim => claim.Type == "Password").Value;

					var userConext = _dbContext.Users.Where(x => x.Username == applicationContext.Username && x.Password == password);

					bool isIdentityLogined = userConext == null ? false : true;

					if (idtoken == null && !isIdentityLogined)
					{
						GetUnauthorizedStatus(context);
					}
				}
				//GetUnauthorizedStatus(context);
			}
			else{
				// not logged in
				GetUnauthorizedStatus(context);
			}
		}

		private void GetUnauthorizedStatus(AuthorizationFilterContext context)
		{
			context.Result = new JsonResult(new { message = "Unauthorized" })
			{
				StatusCode = StatusCodes.Status401Unauthorized
			};
		}
	}
}
