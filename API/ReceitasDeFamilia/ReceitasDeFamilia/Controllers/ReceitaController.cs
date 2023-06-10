using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReceitasDeFamilia.Models;
using ReceitasDeFamilia.Repositories;
using ReceitasDeFamilia.Services;

namespace ReceitasDeFamilia.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ReceitaController : ControllerBase
    {
        private readonly IReceitaRepository _receitaRepository;
        private readonly IUserService _userService;

        public ReceitaController(IReceitaRepository receitaRepository, IUserService userService)
        {
            _receitaRepository = receitaRepository;
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult<ReceitaViewModel>> Post(ReceitaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }

            model.SetUsuarioCriacaoFromService(_userService);

            var receita = _receitaRepository.Add(model, userId: _userService.GetId());

            if (receita == null)
            {
                return BadRequest(new { message = "Falha ao salvar." });
            }

            return receita;
        }

        [HttpDelete]
        [Route("{receitaId}")]
        public async Task<ActionResult> Delete(int receitaId)
        {
            if (!IsReceitaOwnedByUser(receitaId))
            {
                return Conflict(new { message = "Falha ao deletar." });
            }

            var isDeleted = _receitaRepository.Delete(receitaId);

            if (!isDeleted)
            {
                return BadRequest(new { message = "Falha ao deletar." });
            }

            return Ok();
        }

        [HttpPut]
        [Route("{receitaId}")]
        public async Task<ActionResult<ReceitaViewModel>> Update(int receitaId, ReceitaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }

            if (!IsReceitaOwnedByUser(receitaId))
            {
                return Conflict(new { message = "Falha na atualização." });
            }

            model.IdFamilia = receitaId;

            model.Update(_userService);

            var receita = _receitaRepository.Update(model, userId: _userService.GetId());

            if (receita == null)
            {
                return BadRequest(new { message = "Falha na atualização." });
            }

            return Ok(receita);
        }

        [HttpGet]
        [Route("{receitaId?}")]
        public async Task<IActionResult> Get(int? receitaId)
        {
            if (receitaId == null)
            {
                return Ok(_receitaRepository.Get(userId: _userService.GetId()));
            }

            var receita = _receitaRepository.Get(receitaId.Value, userId: _userService.GetId());

            if (receita == null)
            {
                return BadRequest();
            }

            return Ok(receita);
        }

        private bool IsReceitaOwnedByUser(int receitaId)
        {
            var famFromDb = _receitaRepository.Get(receitaId, userId: _userService.GetId());

            if (famFromDb == null || famFromDb?.UsuarioCriacao == _userService.GetIdentificacao())
            {
                return true;
            }

            return false;
        }
    }
}
