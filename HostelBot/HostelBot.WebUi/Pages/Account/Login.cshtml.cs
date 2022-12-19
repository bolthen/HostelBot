using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Security.Claims;
using HostelBot.Domain.Domain;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUi.Pages.Account
{
    public class Login : PageModel
    {
        [BindProperty]
        public HostelManager Manager { get; set; }
        
        public void OnGet()
        {
        
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            // TODO Валидация Заведующей    Manager.Login и Manager.Password

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, Manager.Login),
                new Claim("Hostel", "1")
            };
            var identity = new ClaimsIdentity(claims, "CookieAuth");
            var claimPrincipal = new ClaimsPrincipal(identity);
            
            await HttpContext.SignInAsync("CookieAuth", claimPrincipal);

            return RedirectToPage("/Index");
        }
    }

    public class HostelManager : IdentityUser
    {
        [Required]
        [Display(Name = "Логин")]
        public string Login { get; set; }
        
        [Required]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
    }
}