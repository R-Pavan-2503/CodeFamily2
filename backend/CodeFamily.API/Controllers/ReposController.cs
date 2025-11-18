using Microsoft.AspNetCore.Mvc;
using CodeFamily.Core.Services;
using CodeFamily.API.DTOs;

namespace CodeFamily.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReposController : ControllerBase
{
    private readonly IGitService _gitService;

    public ReposController(IGitService gitService)
    {
        _gitService = gitService;
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
}