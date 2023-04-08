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
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserService _userService;

        public CategoryController(ICategoryRepository categoryRepository, IUserService userService)
        {
            _categoryRepository = categoryRepository;
            _userService = userService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<CategoryViewModel>> Post(CategoryViewModel model)
        {
            if (!ModelState.IsValid || (model?.UserId > 0 && model?.UserId != _userService.GetId()))
            {
                return BadRequest(model);
            }

            if (model?.UserId == 0)
            {
                model.UserId = _userService.GetId();
            }

            var category = _categoryRepository.Add(model);

            if (category == null)
            {
                return BadRequest(new { message = "Falha ao salvar." });
            }

            return category;
        }

        [HttpDelete]
        [Route("{categoryId}")]
        //[ServiceFilter(typeof(UserValidationActionFilter))]
        public async Task<ActionResult> Delete(int categoryId)
        {
            if (!IsCategoryOwnedByUser(categoryId))
            {
                return BadRequest(new { message = "Falha ao deletar." });
            }

            var isDeleted = _categoryRepository.Delete(categoryId);

            if (!isDeleted)
            {
                return BadRequest(new { message = "Falha ao deletar." });
            }

            return Ok();
        }

        [HttpPut]
        [Route("{categoryId}")]
        //[ServiceFilter(typeof(UserValidationActionFilter))]
        public async Task<ActionResult<CategoryViewModel>> Update(int categoryId, CategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }

            if (!IsCategoryOwnedByUser(categoryId))
            {
                return BadRequest(new { message = "Falha na atualiza��o." });
            }

            if (model?.UserId == 0 && model?.UserId != _userService.GetId())
            {
                model.UserId = _userService.GetId();
            }

            model.CategoryId = categoryId;

            model.Update(_userService);

            var category = _categoryRepository.Update(model);

            if (category == null)
            {
                return BadRequest(new { message = "Falha na atualiza��o." });
            }

            return Ok(category);
        }

        [HttpGet]
        [Route("{categoryId}")]
        //[ServiceFilter(typeof(UserValidationActionFilter))]
        public async Task<ActionResult<CategoryViewModel>> Get(int categoryId)
        {
            var category = _categoryRepository.Get(categoryId);

            if (category == null || category?.UserId != _userService.GetId())
            {
                return BadRequest();
            }

            return Ok(category);
        }

        private bool IsCategoryOwnedByUser(int categoryId)
        {
            var catFromDb = _categoryRepository.Get(categoryId);

            if (catFromDb == null || catFromDb?.UserId == _userService.GetId())
            {
                return true;
            }

            return false;
        }
    }
}