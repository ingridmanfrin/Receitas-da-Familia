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
    public class FamiliaController : ControllerBase
    {
        private readonly IFamiliaRepository _familiaRepository;
        private readonly IUserService _userService;

        public FamiliaController(IFamiliaRepository familiaRepository, IUserService userService)
        {
            _familiaRepository = familiaRepository;
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult<FamiliaViewModel>> Post(FamiliaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }
            
            model.SetUsuarioCriacaoFromService(_userService);

            var familia = _familiaRepository.Add(model);

            if (familia == null)
            {
                return BadRequest(new { message = "Falha ao salvar." });
            }

            return familia;
        }

        [HttpDelete]
        [Route("{familiaId}")]
        public async Task<ActionResult> Delete(int familiaId)
        {
            if (!IsFamiliaOwnedByUser(familiaId))
            {
                return BadRequest(new { message = "Falha ao deletar." });
            }

            var isDeleted = _familiaRepository.Delete(familiaId);

            if (!isDeleted)
            {
                return BadRequest(new { message = "Falha ao deletar." });
            }

            return Ok();
        }

        [HttpPut]
        [Route("{familiaId}")]
        public async Task<ActionResult<FamiliaViewModel>> Update(int familiaId, FamiliaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }

            if (!IsFamiliaOwnedByUser(familiaId))
            {
                return BadRequest(new { message = "Falha na atualização." });
            }

            model.IdFamilia = familiaId;

            model.Update(_userService);

            var familia = _familiaRepository.Update(model);

            if (familia == null)
            {
                return BadRequest(new { message = "Falha na atualização." });
            }

            return Ok(familia);
        }

        [HttpGet]
        [Route("{familiaId?}")]
        public async Task<IActionResult> Get(int? familiaId)
        {
            if (familiaId == null)
            {
                return Ok(_familiaRepository.Get());
            }

            var familia = _familiaRepository.Get(familiaId.Value);

            if (familia == null)
            {
                return BadRequest();
            }

            return Ok(familia);
        }

        private bool IsFamiliaOwnedByUser(int familiaId)
        {
            var famFromDb = _familiaRepository.Get(familiaId);

            if (famFromDb == null || famFromDb?.UsuarioCriacao == _userService.GetIdentificacao())
            {
                return true;
            }

            return false;
        }
    }
}