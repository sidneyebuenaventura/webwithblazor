using DigitalHubUI.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Net.Http.Headers;
using Progress.Sitefinity.RestSdk;

namespace DigitalHubUI.Filters;

public class SitefinitySdkFilter : IAsyncActionFilter
{
	public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
	{
		if (context.HttpContext.Request.Path.Value.StartsWith("/api") && context.Controller is ControllerWithSitefinitySdk controller)
		{
			// necessary to initialize this. automatically done for page requests
			var args = new RequestArgs();
			var requestCookie = context.HttpContext.Request.Headers[HeaderNames.Cookie];
			if (!string.IsNullOrEmpty(requestCookie))
			{
				args.AdditionalHeaders.Add(HeaderNames.Cookie, requestCookie);
			}
			await controller.RestClient.Init(args);
		}
		await next();
	}
}