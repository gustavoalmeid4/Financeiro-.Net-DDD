using Entities.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Entities.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UsersController(UserManager<ApplicationUser> userManager , SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/AdicionaUsuario")]
        public async Task<IActionResult> AdicionaUsuario([FromBody] Login login)
        {
            if (string.IsNullOrEmpty(login.senha) ||
                string.IsNullOrEmpty(login.email) ||
                string.IsNullOrEmpty(login.cpf))
            {
                return Ok("Preencha todos os dados!");
            }

            ApplicationUser user = new ApplicationUser
            {
                Email = login.email,
                CPF = login.cpf,
                UserName = login.email
            };
            
            var result = await _userManager.CreateAsync(user , login.senha);

            if (result.Errors.Any())
                return Ok(result.Errors);

            //Geração de confirmação no email.
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            //Retorno do email.
            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));

            var response_Return = await _userManager.ConfirmEmailAsync(user, code);

            if (response_Return.Succeeded)
                return Ok("Usuario criado com sucesso.");
            else
                return Ok("Ocorreu um erro!");
        }
    }
}
