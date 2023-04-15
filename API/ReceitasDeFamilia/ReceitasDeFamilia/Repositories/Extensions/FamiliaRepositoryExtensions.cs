using ReceitasDeFamilia.Models;
using ReceitasDeFamilia.Repositories.Entities;
using ReceitasDeFamilia.Services;

namespace ReceitasDeFamilia.Repositories.Extensions
{
    public static class FamiliaRepositoryExtensions
    {
        public static FamiliaViewModel ToFamiliaViewModel(this Familia familia)
        {
            if (familia == null)
            {
                return null;
            }

            return new FamiliaViewModel()
            {
                IdFamilia = familia.IdFamilia,
                Nome = familia.Nome,
                Descricao = familia.Descricao,
                Foto = familia.Foto,
                IsDeleted = familia.FoiDeletado,
                CreatedDatetime = familia.DataCriacao,
                LastEditDatetime = familia.DataAlteracao,
                UsuarioAlteracao = familia.UsuarioAlteracao,
                UsuarioCriacao = familia.UsuarioCriacao
            };
        }

        public static Familia ToFamiliaEntity(this FamiliaViewModel familia)
        {
            if (familia == null)
            {
                return null;
            }

            return new Familia()
            {
                IdFamilia = familia.IdFamilia,
                Nome = familia.Nome,
                Foto= familia.Foto,
                Descricao = familia.Descricao,
                FoiDeletado = familia.IsDeleted,
                DataCriacao = familia.CreatedDatetime,
                DataAlteracao = familia.LastEditDatetime,
                UsuarioCriacao = familia.UsuarioCriacao,
                UsuarioAlteracao = familia.UsuarioAlteracao
            };
        }
        public static Familia UpdateFrom(this Familia familia, FamiliaViewModel model)
        {
            if (model == null)
            {
                return familia;
            }

            if (familia == null)
            {
                familia = new Familia();
            }

            familia.DataAlteracao = model.LastEditDatetime;
            familia.UsuarioAlteracao = model.UsuarioAlteracao;
            familia.Nome = model.Nome;
            familia.Foto = model.Foto;
            familia.Descricao = model.Descricao;

            return familia;
        }
    }
}
