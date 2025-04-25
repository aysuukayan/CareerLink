using CareerLink.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CareerLinkBackend1.Models; 
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.Extensions.DependencyInjection;
using CareerLinkBackend1.Data;



[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = new ApplicationUser
        {
            FullName = model.FullName,
            UserName = model.Email,
            Email = model.Email,
            IsCompany = model.IsCompany,
            CompanyName = model.IsCompany ? model.CompanyName : null,
            Website = model.IsCompany ? model.Website : null,
            Description = model.IsCompany ? model.Description : null
        };

        var result = await _userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return Ok(new { Message = "Kullanıcı başarıyla kaydedildi." });
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

        if (result.Succeeded)
            return Ok(new { message = "Giriş Başarılı" });

        return Unauthorized(new { message = "Geçersiz şifre ya da kullanıcı" });
    }


    [HttpGet("profile")]
    [Authorize]
    public async Task<IActionResult> GetProfile()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
            return NotFound("Kullanıcı bulunamadı.");

        return Ok(new
        {
            user.FullName,
            user.Email,
            user.IsCompany,
            user.CompanyName,
            user.Website,
            user.Description
        });
    }

    [HttpPost("assign-role")]
    public async Task<IActionResult> AssignRole([FromBody] AssignRoleModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
            return NotFound("Kullanıcı bulunamadı.");

        var result = await _userManager.AddToRoleAsync(user, model.Role);
        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return Ok(new { Message = $"{model.Email} kullanıcısına {model.Role} rolü atandı." });
    }



}
