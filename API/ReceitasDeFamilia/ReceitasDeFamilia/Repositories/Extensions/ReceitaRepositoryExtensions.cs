using ReceitasDeFamilia.Models;
using ReceitasDeFamilia.Repositories.Entities;
using ReceitasDeFamilia.Services;

namespace ReceitasDeFamilia.Repositories.Extensions
{
    public static class ReceitaRepositoryExtensions
    {
        public static ReceitaViewModel ToReceitaViewModel(this Receita receita)
            => ToReceitaViewModel(receita, false);

        public static ReceitaViewModel ToReceitaViewModel(this Receita receita, bool favorito)
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
                UsuarioCriacao = receita.UsuarioCriacao,
                Favorito = favorito
            };
        }

        public static Receita ToReceitaEntity(this ReceitaViewModel receita)
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

        public static Receita UpdateFrom(this Receita receita, ReceitaViewModel model)
        {
            if (model == null)
            {
                return receita;
            }

            if (receita == null)
            {
                receita = new Receita();
            }

            receita.DataAlteracao = model.LastEditDatetime;
            receita.UsuarioAlteracao = model.UsuarioAlteracao;
            receita.Nome = model.Nome;
            receita.CriadorReceita = model.CriadorReceita;
            receita.InformacoesAdicionais = model.InformacoesAdicionais;
            receita.Rendimento = model.Rendimento;
            receita.ModoPreparo = model.ModoPreparo;
            receita.Ingredientes = model.Ingredientes;
            receita.TempoPreparoMin = model.TempoPreparoMin;
            receita.IdCategoria = model.IdCategoria;

            return receita;
        }

        public static Favorito ToFavoritoEntity(this FavoritoViewModel favorito)
        {
            if (favorito == null)
            {
                return null;
            }

            return new Favorito()
            {
                IdReceita = favorito.IdReceita,
                IdUsuario = favorito.IdUsuario,

                FoiDeletado = favorito.IsDeleted,
                DataCriacao = favorito.CreatedDatetime,
                DataAlteracao = favorito.LastEditDatetime,
                UsuarioCriacao = favorito.UsuarioCriacao,
                UsuarioAlteracao = favorito.UsuarioAlteracao
            };
        }
    }
}
