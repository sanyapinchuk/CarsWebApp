using System.Web.Http;
using System.Web.Http.Controllers;
using System.Net.Http;
using IdentityModel;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace CarsServer.Helpers
{
	public sealed class AuthAttributeAttribute : Attribute, IAuthorizationFilter
	{
		public void OnAuthorization(AuthorizationFilterContext context)
		{
			var header = context.HttpContext.Request.Headers["secretToken"];
			var temp = "CarsApi".ToSha256();
			if (string.IsNullOrWhiteSpace(header) || header != temp)
			{
				context.Result = new UnauthorizedObjectResult(string.Empty);
			}
		}
	}
}