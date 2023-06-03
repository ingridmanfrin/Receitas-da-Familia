﻿using Microsoft.EntityFrameworkCore;
using ReceitasDeFamilia.Exceptions;
using ReceitasDeFamilia.Models;
using ReceitasDeFamilia.Repositories.Entities;
using ReceitasDeFamilia.Repositories.Extensions;

namespace ReceitasDeFamilia.Repositories
{
    public interface IReceitaRepository
    {
        ReceitaViewModel? Get(int receitaId);
        ICollection<ReceitaViewModel> Get();
        ReceitaViewModel Add(ReceitaViewModel model);
        ReceitaViewModel Update(ReceitaViewModel model);
        bool Delete(int receitaId);
    }
    public class ReceitaRepository : IReceitaRepository
    {

        public ReceitaViewModel? Get(int receitaId)
        {
            using (var context = new ReceitasDeFamiliaDbContext())
            {
                return context.Receitas.Find(receitaId)
                    .ToReceitaViewModel(IsReceitaFavorita(receitaId));
            }
        }

        public ICollection<ReceitaViewModel> Get()
        {
            using (var context = new ReceitasDeFamiliaDbContext())
            {
                return context.Receitas.Where(x => !x.FoiDeletado).Select(x => x.ToReceitaViewModel(IsReceitaFavorita(x.IdReceita))).ToList();
            }
        }

        public ReceitaViewModel Add(ReceitaViewModel model)
        {
            using (var context = new ReceitasDeFamiliaDbContext())
            {
                var entity = model.ToReceitaEntity();
                context.Receitas.Add(entity);
                context.SaveChanges();
                return entity.ToReceitaViewModel(IsReceitaFavorita(entity.IdReceita));
            }
        }

        public ReceitaViewModel Update(ReceitaViewModel model)
        {
            using (var context = new ReceitasDeFamiliaDbContext())
            {
                var receitaFromDB = context.Receitas.Find(model.IdReceita);
                if (receitaFromDB == null)
                {
                    return null;
                }
                receitaFromDB.UpdateFrom(model);
                context.Receitas.Update(receitaFromDB);
                context.SaveChanges();
                return receitaFromDB.ToReceitaViewModel(IsReceitaFavorita(receitaFromDB.IdReceita));
            }
        }

        public bool Delete(int receitaId)
        {
            using (var context = new ReceitasDeFamiliaDbContext())
            {
                var receitaFromDB = context.Receitas.Find(receitaId);
                if (receitaFromDB == null)
                {
                    return false;
                }

                receitaFromDB.FoiDeletado = true;

                context.Receitas.Update(receitaFromDB);
                context.SaveChanges();
                return true;
            }
        }

        private static bool IsReceitaFavorita(int receitaId)
        {
            using (var context = new ReceitasDeFamiliaDbContext())
            {
                return context.Favoritos.Where(x => !x.FoiDeletado && x.IdReceita == receitaId).Any();
            }
        }
    }
}
