using DigitalHubUI.Dto.Course;
using DigitalHubUI.Repositories.Interfaces;
using DigitalHubUI.Services.Interface;
using DigitalHubUI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.Dto;
using IRestClient = Progress.Sitefinity.RestSdk.IRestClient;

namespace DigitalHubUI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CourseController : ControllerWithSitefinitySdk
{
    private readonly IFileRepository _fileRepository;
    private readonly ICourseRepository _courseRepository;
    private readonly ICourseService _courseService;
    private readonly IProfileRepository _profileRepository;

    public CourseController(IRestClient restClient, IFileRepository fileRepository, ICourseRepository courseRepository, 
        ICourseService courseService, IProfileRepository profileRepository)
        : base(restClient)
    {
        _fileRepository = fileRepository;
        _courseRepository = courseRepository;
        _courseService = courseService;
        _profileRepository = profileRepository;
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateCourseRequestDto requestDto)
    {
        if (!CurrentUser.IsAuthenticated)
        {
            return Unauthorized("You must be logged in first to create new course!");
        }
        
        // Create new Course and corresponding Modules, Quiz
        var currentUser = await _profileRepository.GetCurrentUser();
        var createdCourse = await _courseRepository.CreateCourse(currentUser, requestDto);

        // Handle CoverImage upload into Image
        // Relate Course with Image
        ImageDto uploadedCoverImage = null;
        if (requestDto.CoverImageUpload != null)
        {
            var imageInfo = new Dictionary<string, string>
            {
                // Old incorrect parent Id
                // {"ParentId", "73e448de-c66a-40a4-8c79-e37ed16957fb"},
                // Course Cover Images library id
                {"ParentId", "21451e80-d564-4749-8533-b3f780541c46"},
                {"Title", requestDto.Title},
                {"AlternativeText", requestDto.Title}
            };
            uploadedCoverImage = await _fileRepository.UploadCourseCoverImage(imageInfo, requestDto.CoverImageUpload, createdCourse);
            await _restClient.RefreshItem(createdCourse);
        }
        await _courseService.EnableTouchPointTracking(createdCourse);

        var response = new CreateCourseResponseDto(createdCourse, uploadedCoverImage);
        return Json(response);
    }

    [HttpPut]
    public async Task<IActionResult> Put(UpdateCourseRequestDto requestDto)
    {
        if (!CurrentUser.IsAuthenticated)
        {
            return Unauthorized("You must be logged in first to update this course!");
        }

        // Update Course
        var currentUser = await _profileRepository.GetCurrentUser();
        var course = await _courseRepository.UpdateCourse(currentUser, requestDto);
        if (course == null) return BadRequest("Course update failed.");

        if (!course.Owner.Equals(currentUser.Id))
        {
            return Unauthorized("You are not authorized to update this course!");
        }

        // Handle CoverImage upload into Image
        // Relate Course with Image
        if (requestDto.CoverImageUpload == null) return Json(new UpdateCourseResponseDto(course, null));
        var imageInfo = new Dictionary<string, string>
        {
            // Old incorrect parent Id
            // {"ParentId", "73e448de-c66a-40a4-8c79-e37ed16957fb"},
            // Course Cover Images library id
            {"ParentId", "21451e80-d564-4749-8533-b3f780541c46"},
            {"Title", requestDto.Title},
            {"AlternativeText", requestDto.Title}
        };
        var uploadedCoverImage = await _fileRepository.UploadCourseCoverImage(imageInfo, requestDto.CoverImageUpload, course);
        await _restClient.RefreshItem(course);

        return Json(new UpdateCourseResponseDto(course, uploadedCoverImage));
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery(Name = "id")] string Id)
    {
        if (!CurrentUser.IsAuthenticated)
        {
            return Unauthorized("You must be logged in first to delete this course!");
        }
        var currentUser = await _profileRepository.GetCurrentUser();
        var course = await _courseRepository.GetById(Id, 0);
        if (!currentUser.Id.Equals(course.Owner))
        {
            return Unauthorized("You are not authorized to delete this course!");
        }
        await _courseRepository.DeleteCourse(Id);
        return Ok();
    }

    [HttpGet]
    [Route("courses")]
    public async Task<IActionResult> SeeMoreForCategory(
        [FromQuery(Name = "categoryId")] string categoryId,
        [FromQuery(Name = "count")] int count,
        [FromQuery(Name = "excludedCoursesIds")] string excludedCoursesIds)
    {
        var excludedCoursesIdsList =
            excludedCoursesIds.Equals("_") ? Array.Empty<string>() : excludedCoursesIds.Split(",");
        var remainedCoursesIds = _courseRepository.GetCoursesIdsByCategory(categoryId, excludedCoursesIdsList);
        var courses = await _courseRepository.GetByIds(remainedCoursesIds.ToArray(), count);
        if (remainedCoursesIds.Count - courses.Count > 0)
        {
            Response.Headers.Add("SeeMore", "true");
        }
        return PartialView("~/Views/Shared/DisplayTemplates/CourseViewModelList.cshtml", courses);
    }

    [HttpGet]
    [Route("addModule")]
    public async Task<IActionResult> AddModule()
    {
        return PartialView("~/Views/Shared/DisplayTemplates/ModuleLinkViewModel.cshtml", 
            new ModuleViewModel("", "New Module", "", 999/*there should be no more than 999 modules in a course*/, 0));
    }

    [HttpGet]
    [Route("addQuestion")]
    public async Task<IActionResult> AddQuestion()
    {
        return PartialView("~/Views/Shared/DisplayTemplates/QuizQuestionViewModel.cshtml", 
            new QuizQuestionViewModel("", "", "", 999/*there should be no more than 999 questions in a quiz*/, null));
    }

    [HttpGet]
    [Route("addOption")]
    public async Task<IActionResult> AddOption()
    {
        return PartialView("~/Views/Shared/DisplayTemplates/QuizQuestionOptionViewModel.cshtml", 
            new QuizQuestionOptionViewModel("", "", false, 999/*there should be no more than 999 options in a questions*/));
    }

    [HttpGet]
    [Route("joinCourse")]
    public async Task<IActionResult> JoinCourse([FromQuery(Name = "Id")] string courseId)
    {
        var joinCourseResponseDto = await _courseRepository.JoinCourse(_profileRepository.GetCurrentUserProfile().Result, courseId);
        return Json(joinCourseResponseDto);
    }

    [HttpGet]
    [Route("markModuleCompleted")]
    public async Task<IActionResult> MarkModuleCompleted([FromQuery(Name = "Id")] string moduleId)
    {
        var moduleCompletionDto = await _courseRepository.MarkModuleCompleted(_profileRepository.GetCurrentUserProfile().Result, moduleId);
        return Json(moduleCompletionDto);
    }

    [HttpPost]
    [Route("submitAnswers")]
    public async Task<IActionResult> SubmitAnswers(SubmitAnswerRequestDto requestDto, [FromQuery(Name = "Id")] string quizId)
    {
        var correctAnswer = await _courseRepository.SubmitAnswers(_profileRepository.GetCurrentUserProfile().Result, requestDto, quizId);
        return Json(correctAnswer);
    }

    [HttpGet]
    public async Task<IActionResult> Search([FromQuery(Name = "searchText")] string searchText,
        [FromQuery(Name = "excludedCourseIds")] string? excludedCourseIdsStr)
    {
        var excludedCourseIds = String.IsNullOrEmpty(excludedCourseIdsStr) ? new string[] { } : excludedCourseIdsStr.Split(",");
        // 1st search returns 24 items, "Show more" returns 12 items
        var count = excludedCourseIds.Length == 0 ? 24 : 12;
        List<CourseViewModel> courses;

        if (!String.IsNullOrEmpty(searchText) && !String.IsNullOrWhiteSpace(searchText))
        {
            var remainedCourseIds = _courseRepository.SearchCourseIdsByText(searchText, excludedCourseIds).Result;
            if (remainedCourseIds.Count > 0)
            {
                courses = await _courseRepository.GetByIds(remainedCourseIds.ToArray(), count);
                if (remainedCourseIds.Count - courses.Count > 0)
                {
                    Response.Headers.Add("HasMore", "true");
                }
                return PartialView("~/Views/Shared/DisplayTemplates/CourseViewModelList.cshtml", courses);
            }
        }
        
        // search trending posts if there is no posts found with searchText
        Response.Headers.Add("Trending", "true");
        courses = _courseRepository.SearchTrendingCourses(12);
        return PartialView("~/Views/Shared/DisplayTemplates/CourseViewModelList.cshtml", courses);
    }
}
