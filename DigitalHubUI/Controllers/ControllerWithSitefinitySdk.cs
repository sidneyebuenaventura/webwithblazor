using DigitalHubUI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.Clients.Users.Dto;
using Progress.Sitefinity.RestSdk.Dto.Content;

namespace DigitalHubUI.Controllers;

public abstract class ControllerWithSitefinitySdk : Controller
{
	protected IRestClient _restClient;
	protected UserDto _currentUser;
	protected BlogDto _currentBlog;

	protected ControllerWithSitefinitySdk(IRestClient restClient)
	{
		_restClient = restClient;
	}

	public IRestClient RestClient => _restClient;

	public UserDto CurrentUser
	{
		get
		{
			if (_currentUser == null)
			{
				_currentUser = _restClient.Users().GetCurrentUser().Result;
			}
			return _currentUser;
		}
		// NOTE: Only use setter to setup Controller tests
		set => _currentUser = value;
	}

	public BlogDto CurrentBlog
	{
		get
		{
			if (_currentBlog == null)
			{
				_currentBlog = new BlogRepository(_restClient).GetBlogsByOwner(CurrentUser.Id).Result.FirstOrDefault();
			}
			return _currentBlog;
		}
		set => _currentBlog = value;
	}

	public RequestArgs InitRequestArgs()
	{
		var args = new RequestArgs();
		var requestCookie = this.HttpContext.Request.Headers[HeaderNames.Cookie];
		if (!string.IsNullOrEmpty(requestCookie))
		{
			args.AdditionalHeaders.Add(HeaderNames.Cookie, requestCookie);	
		}
		return args;
	}

}