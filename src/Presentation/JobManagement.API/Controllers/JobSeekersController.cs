using System.Security.Claims;
using JobManagement.Application.DTOs.JobSeeker;
using JobManagement.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "JobSeeker")]
public class JobSeekersController : ControllerBase
{
    private readonly IJobSeekerService _jobSeekerService;

    public JobSeekersController(IJobSeekerService jobSeekerService)
    {
        _jobSeekerService = jobSeekerService;
    }

    [HttpPost("create-cv")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> CreateCv([FromForm] CreateCvDto dto)
    {
        var userId = GetUserId();

        if (userId == null)
            return Unauthorized();

        var response = await _jobSeekerService.CreateCvAsync(userId.Value, dto);

        if (!response.Success)
            return BadRequest(response);

        return Ok(response);
    }

    [HttpGet("get-own-cv")]
    public async Task<IActionResult> GetOwnCv()
    {
        var userId = GetUserId();

        if (userId == null)
            return Unauthorized();

        var response = await _jobSeekerService.GetOwnCvAsync(userId.Value);

        if (!response.Success)
            return BadRequest(response);

        return Ok(response);
    }

    private Guid? GetUserId()
    {
        var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(userIdValue, out var userId))
            return null;

        return userId;
    }
}
