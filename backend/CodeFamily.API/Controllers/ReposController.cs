using Microsoft.AspNetCore.Mvc;
using CodeFamily.Core.Services;
using CodeFamily.API.DTOs;
using System.Net;

namespace CodeFamily.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReposController : ControllerBase
{
    private readonly IGitService _gitService;
    private readonly ICommitService _commitService;


    public ReposController(IGitService gitService)
    {
        _gitService = gitService;
        _commitService = commitService;
    }

    [HttpPost("connect")]
    public IActionResult ConnectRepo([FromBody] AnalyzeRepoRequest request)
    {
        if (string.IsNullOrEmpty(request.Url))
        {
            return BadRequest("Repository URL is required.");
        }

        // Trigger the cloning process
        // In a real app, this should be backgrounded (Fire-and-Forget),
        // but for this step, we will wait to confirm it works.
        var localPath = _gitService.CloneRepository(request.Url);

        return Ok(new { Message = "Repository cloned successfully", LocalPath = localPath });
    }

    [HttpGet("commits")]
    public IActionResult GetCommits([FromQuery] string localPath)
    {
        if (string.IsNullOrEmpty(localPath))
        {
            return BadRequest("Local path is required.");
        }

        try
        {
            // In a real app, we would validate this path is safe!
            // For MVP/Dev, we trust the input for now.
            var logs = _commitService.GetCommitLog(localPath);

            // Return just the top 20 to keep response light
            return Ok(logs.Take(20));
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error reading commits: {ex.Message}");
        }
    }
}