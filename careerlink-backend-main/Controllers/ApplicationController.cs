using CareerLinkBackend1.Data;
using CareerLink.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

using CareerLinkBackend1.Data;

[Route("api/[controller]")]
[ApiController]
public class ApplicationController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ApplicationController(ApplicationDbContext context)
    {
        _context = context;
    }

 
    [HttpPost]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> ApplyToJob([FromBody] int jobId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (await _context.Applications.AnyAsync(a => a.JobPostingId == jobId && a.ApplicantId == userId))
        {
            return BadRequest("Zaten bu ilana başvurdunuz.");
        }

        var application = new Application
        {
            JobPostingId = jobId,
            ApplicantId = userId
        };

        _context.Applications.Add(application);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Başvuru başarılı!" });
    }

   
    [HttpGet]
    [Authorize(Roles = "Employer")]
    public async Task<IActionResult> GetApplicants([FromQuery] int jobId)
    {
        var applicants = await _context.Applications
            .Include(a => a.Applicant)
            .Where(a => a.JobPostingId == jobId)
            .Select(a => new {
                a.Id,
                a.Applicant.FullName,
                a.Applicant.Email,
                a.AppliedAt
            })
            .ToListAsync();

        return Ok(applicants);
    }

  
    [HttpDelete("{id}")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> WithdrawApplication(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var application = await _context.Applications.FirstOrDefaultAsync(a => a.Id == id && a.ApplicantId == userId);

        if (application == null)
        {
            return NotFound("Başvuru bulunamadı.");
        }

        _context.Applications.Remove(application);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Başvuru geri çekildi." });
    }
}
