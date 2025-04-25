
using CareerLink.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CareerLinkBackend1.Data;

[Route("api/[controller]")]
[ApiController]
public class JobPostingController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public JobPostingController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    [Authorize(Roles = "Employer")]
    public async Task<IActionResult> CreateJobPosting([FromBody] JobPosting jobPosting)
    {
        if (jobPosting == null)
        {
            return BadRequest("Geçersiz iş ilanı.");
        }

        jobPosting.CreatedAt = DateTime.UtcNow; 
        _context.JobPostings.Add(jobPosting);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetJobPosting), new { id = jobPosting.Id }, jobPosting);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllJobPostings()
    {
        var jobPostings = await _context.JobPostings.ToListAsync();
        return Ok(jobPostings);
    }

    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetJobPosting(int id)
    {
        var jobPosting = await _context.JobPostings.FindAsync(id);
        if (jobPosting == null)
        {
            return NotFound("İş ilanı bulunamadı.");
        }
        return Ok(jobPosting);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Employer")]
    public async Task<IActionResult> UpdateJobPosting(int id, [FromBody] JobPosting updatedJobPosting)
    {
        var existingJob = await _context.JobPostings.FindAsync(id);

        if (existingJob == null)
        {
            return NotFound("Güncellenecek iş ilanı bulunamadı.");
        }

        existingJob.Title = updatedJobPosting.Title;
        existingJob.Description = updatedJobPosting.Description;
        existingJob.CompanyName = updatedJobPosting.CompanyName;
        existingJob.Location = updatedJobPosting.Location;
        existingJob.Salary = updatedJobPosting.Salary;

        _context.JobPostings.Update(existingJob);
        await _context.SaveChangesAsync();

        return Ok(new { message = "İş ilanı başarıyla güncellendi.", job = existingJob });
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Employer")]
    public async Task<IActionResult> DeleteJobPosting(int id)
    {
        var jobPosting = await _context.JobPostings.FindAsync(id);

        if (jobPosting == null)
        {
            return NotFound("Silinecek iş ilanı bulunamadı.");
        }

        _context.JobPostings.Remove(jobPosting);
        await _context.SaveChangesAsync();

        return Ok(new { message = "İş ilanı başarıyla silindi." });
    }



}
