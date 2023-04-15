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
    public class CategoriaReceitaController : ControllerBase
    {
        private readonly ICategoriaReceitaRepository _categoryRepository;
        private readonly IUserService _userService;

        public CategoriaReceitaController(ICategoriaReceitaRepository categoryRepository, IUserService userService)
        {
            _categoryRepository = categoryRepository;
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<CategoriaReceitaViewModel>> Get()
        {
            var category = _categoryRepository.Get();

            return Ok(category);
        }
    }
}