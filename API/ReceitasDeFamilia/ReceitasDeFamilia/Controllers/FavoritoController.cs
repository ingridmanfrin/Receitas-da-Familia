using ReceitasDeFamilia.Models;
using ReceitasDeFamilia.Repositories;
using ReceitasDeFamilia.Repositories.Entities;
using ReceitasDeFamilia.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Security.Claims;

namespace ReceitasDeFamilia.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class FavoritoController : ControllerBase
    {
        private readonly IFavoritoRepository _favoritoRepository;
        private readonly IUserService _userService;

        public FavoritoController(IFavoritoRepository favoritoRepository, IUserService userService)
        {
            _favoritoRepository = favoritoRepository;
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var response = _favoritoRepository.Get(_userService.GetId());

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> Post(FavoritoViewModel model)
        {
            model.SetUsuarioCriacaoFromService(_userService);
            model.IdUsuario = _userService.GetId();

            var response = _favoritoRepository.Add(model);

            return Ok(response);
        }

        [HttpDelete]
        [Route("{receitaId}")]
        public async Task<ActionResult> Delete(int receitaId)
        {
            var model = new FavoritoViewModel
            {
                IdReceita = receitaId,
                IdUsuario = _userService.GetId()
            };
            var isDeleted = _favoritoRepository.Delete(model);

            if (!isDeleted)
            {
                return BadRequest(new { message = "Falha ao deletar." });
            }

            return Ok();
        }
    }
}