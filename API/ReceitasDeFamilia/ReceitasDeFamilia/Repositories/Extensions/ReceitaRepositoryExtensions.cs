using ReceitasDeFamilia.Models;
using ReceitasDeFamilia.Repositories.Entities;
using ReceitasDeFamilia.Services;

namespace ReceitasDeFamilia.Repositories.Extensions
{
    public static class ReceitaRepositoryExtensions
    {
        public static ReceitaViewModel ToFamiliaViewModel(this Receita receita)
        {
            if (receita == null)
            {
                return null;
            }

            return new ReceitaViewModel()
            {
                IdReceita = receita.IdReceita,
                IdFamilia = receita.IdFamilia,
                IdCategoria = receita.IdCategoria,
                Nome = receita.Nome,
                CriadorReceita = receita.CriadorReceita,
                InformacoesAdicionais = receita.InformacoesAdicionais,
                Ingredientes = receita.Ingredientes,
                ModoPreparo = receita.ModoPreparo,
                Rendimento = receita.Rendimento,
                TempoPreparoMin = receita.TempoPreparoMin,
                IsDeleted = receita.FoiDeletado,
                CreatedDatetime = receita.DataCriacao,
                LastEditDatetime = receita.DataAlteracao,
                UsuarioAlteracao = receita.UsuarioAlteracao,
                UsuarioCriacao = receita.UsuarioCriacao
            };
        }

        public static Receita ToFamiliaEntity(this ReceitaViewModel receita)
        {
            if (receita == null)
            {
                return null;
            }

            return new Receita()
            {
                IdReceita = receita.IdReceita,
                IdFamilia = receita.IdFamilia,
                IdCategoria = receita.IdCategoria,
                Nome = receita.Nome,
                CriadorReceita = receita.CriadorReceita,
                InformacoesAdicionais = receita.InformacoesAdicionais,
                Ingredientes = receita.Ingredientes,
                ModoPreparo = receita.ModoPreparo,
                Rendimento = receita.Rendimento,
                TempoPreparoMin = receita.TempoPreparoMin,

                FoiDeletado = receita.IsDeleted,
                DataCriacao = receita.CreatedDatetime,
                DataAlteracao = receita.LastEditDatetime,
                UsuarioCriacao = receita.UsuarioCriacao,
                UsuarioAlteracao = receita.UsuarioAlteracao
            };
        }
        public static Receita UpdateFrom(this Receita familia, ReceitaViewModel model)
        {
            if (model == null)
            {
                return familia;
            }

            if (familia == null)
            {
                familia = new Receita();
            }

            familia.DataAlteracao = model.LastEditDatetime;
            familia.UsuarioAlteracao = model.UsuarioAlteracao;
            familia.Nome = model.Nome;

            //Nome = receita.Nome,
            //    CriadorReceita = receita.CriadorReceita,
            //    InformacoesAdicionais = receita.InformacoesAdicionais,
            //    Ingredientes = receita.Ingredientes,
            //    ModoPreparo = receita.ModoPreparo,
            //    Rendimento = receita.Rendimento,
            //    TempoPreparoMin = receita.TempoPreparoMin,

            return familia;
        }
    }
}
