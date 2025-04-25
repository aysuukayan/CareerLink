using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using CareerLinkBackend1.Data;
using CareerLink.Models;
using CareerLinkBackend1.Models;

[ApiController]
[Route("api/[controller]")]
public class SavedJobsController : ControllerBase
{
    private readonly ApplicationDbContext _context;


    public SavedJobsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> SaveJob([FromBody] SavedJobDto dto)
    {
        var userId = GetUserId(); 

        var savedJob = new SavedJob
        {
            UserId = userId,
            JobId = dto.JobId
        };

        _context.SavedJobs.Add(savedJob);
        await _context.SaveChangesAsync();

        return Ok(savedJob);
    }

   
    [HttpGet]
    public async Task<IActionResult> GetSavedJobs()
    {
        var userId = GetUserId();

        var savedJobs = await _context.SavedJobs
            .Where(sj => sj.UserId == userId)
            .Include(sj => sj.Job) 
            .ToListAsync();

        return Ok(savedJobs);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSavedJob(int id)
    {
        var userId = GetUserId();

        var savedJob = await _context.SavedJobs
            .FirstOrDefaultAsync(sj => sj.Id == id && sj.UserId == userId);

        if (savedJob == null)
            return NotFound("Kaydedilen iş ilanı bulunamadı.");

        _context.SavedJobs.Remove(savedJob);
        await _context.SaveChangesAsync();

        return NoContent();
    }


    private int GetUserId()
    {
        
        return int.Parse(User.FindFirst("id")?.Value ?? "0");
    }
}
