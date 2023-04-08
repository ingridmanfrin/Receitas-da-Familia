using ReceitasDeFamilia.Models;
using ReceitasDeFamilia.Repositories;
using ReceitasDeFamilia.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace ReceitasDeFamilia.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [AllowAnonymous]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginProvider;
        public LoginController(ILoginService loginProvider)
        {
            _loginProvider = loginProvider;
        }

        [HttpPost]
        public async Task<ActionResult<TokenResponse>> Post(LoginViewModel model)
        {
            var tokenResponse = _loginProvider.Login(model);

            if (tokenResponse == null)
                return NotFound(new { message = "Usu�rio ou senha inv�lidos" });

            return tokenResponse;
        }
    }
}