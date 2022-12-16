using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using iTextSharp.text.pdf.qrcode;
using Microsoft.AspNetCore.Authentication;
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
            
            // TODO Валидация Заведующей

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "admin")
            };
            var identity = new ClaimsIdentity(claims, "CookieAuth");
            var claimPrincipal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync("CookieAuth", claimPrincipal);

            return RedirectToPage("/Index");
        }
    }

    public class HostelManager
    {
        [Required]
        [Display(Name = "Логин")]
        public string UserName { get; set; }
        
        [Required]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
    }
}