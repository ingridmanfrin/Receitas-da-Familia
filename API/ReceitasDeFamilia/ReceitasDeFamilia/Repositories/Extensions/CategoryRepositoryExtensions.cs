using ReceitasDeFamilia.Models;
using ReceitasDeFamilia.Repositories.Entities;

namespace ReceitasDeFamilia.Repositories.Extensions
{
    public static class CategoryRepositoryExtensions
    {
        public static CategoriaReceitaViewModel ToCategoriaReceitaViewModel(this CategoriasReceita category)
        {
            if (category == null)
            {
                return null;
            }

            return new CategoriaReceitaViewModel()
            {
                CategoryId = category.IdCategoria,
                Nome = category.Nome,
                IsDeleted = category.FoiDeletado,
                CreatedDatetime = category.DataCriacao,
                LastEditDatetime = category.DataAlteracao,
                UsuarioAlteracao = category.UsuarioAlteracao,
                UsuarioCriacao = category.UsuarioCriacao
            };
        }
    }
}
