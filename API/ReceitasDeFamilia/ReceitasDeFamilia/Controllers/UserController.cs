using ReceitasDeFamilia.Models;
using ReceitasDeFamilia.Repositories;
using ReceitasDeFamilia.Repositories.Entities;
using ReceitasDeFamilia.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace ReceitasDeFamilia.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;

        public UserController(IUserRepository userRepository, IUserService userService)
        {
            _userRepository = userRepository;
            _userService = userService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<UserViewModel>> Post(UserViewModel model)
        {
            //Todo: add validation to new users, also add rateLimiter
            if (!ModelState.IsValid || model.Email.IsNullOrEmpty())
            {
                return BadRequest(model);
            }

            model.SetUsuarioCriacaoFromService(_userService);

            model.Senha = SaltService.HashPasword(model.Senha, out var salt);
            model.PasswordSalt = salt;

            var user = _userRepository.Add(model);
            user.Senha = null;

            if (user == null)
            {
                return BadRequest(new { message = "Falha ao salvar." });
            }

            return user;
        }

        [HttpDelete]
        [Route("{userId}")]
        public async Task<ActionResult> Delete(int userId)
        {
            if (!IsLoggedUserTheTarget(userId))
            {
                return BadRequest();
            }

            var isDeleted = _userRepository.Delete(userId);

            if (!isDeleted)
            {
                return BadRequest(new { message = "Falha ao deletar." });
            }

            return Ok();
        }

        [HttpPut]
        [Route("{userId}")]
        public async Task<ActionResult<UserViewModel>> Update(int userId, UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }

            if (!IsLoggedUserTheTarget(userId))
            {
                return BadRequest();
            }

            model.UserId = userId;
            model.Update(_userService);

            var user = _userRepository.Update(model);

            if (user == null)
            {
                return BadRequest(new { message = "Falha na atualiza��o." });
            }
            user.Senha = null;

            return Ok(user);
        }

        [HttpGet]
        [Route("{userId}")]
        public async Task<ActionResult<UserViewModel>> Get(int userId)
        {
            if (!IsLoggedUserTheTarget(userId))
            {
                return BadRequest();
            }

            var user = _userRepository.Get(userId);

            if (user == null)
            {
                return NotFound(new { message = "Usu�rio n�o encontrado" });
            }

            user.Senha = null;
            user.PasswordSalt = null;

            return Ok(user);
        }

        private bool IsLoggedUserTheTarget(int userId)
        {
            if (_userService.GetId() != userId)
            {
                return false;
            }
            return true;
        }
    }
}