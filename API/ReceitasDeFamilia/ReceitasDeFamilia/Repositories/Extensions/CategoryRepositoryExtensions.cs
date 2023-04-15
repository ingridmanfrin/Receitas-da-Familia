using ReceitasDeFamilia.Models;
using ReceitasDeFamilia.Repositories.Entities;

namespace ReceitasDeFamilia.Repositories.Extensions
{
    public static class CategoryRepositoryExtensions
    {
        public static CategoriaReceitaViewModel ToCategoriaReceitaViewModel(this CategoriasReceita categoria)
        {
            if (categoria == null)
            {
                return null;
            }

            return new CategoriaReceitaViewModel()
            {
                CategoryId = categoria.IdCategoria,
                Nome = categoria.Nome,
                IsDeleted = categoria.FoiDeletado,
                CreatedDatetime = categoria.DataCriacao,
                LastEditDatetime = categoria.DataAlteracao,
                UsuarioAlteracao = categoria.UsuarioAlteracao,
                UsuarioCriacao = categoria.UsuarioCriacao
            };
        }
    }
}
