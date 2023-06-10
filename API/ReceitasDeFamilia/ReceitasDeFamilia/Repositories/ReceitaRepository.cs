using Microsoft.EntityFrameworkCore;
using ReceitasDeFamilia.Exceptions;
using ReceitasDeFamilia.Models;
using ReceitasDeFamilia.Repositories.Entities;
using ReceitasDeFamilia.Repositories.Extensions;
using ReceitasDeFamilia.Services;

namespace ReceitasDeFamilia.Repositories
{
    public interface IReceitaRepository
    {
        ReceitaViewModel? Get(int receitaId, int userId = 0);
        ICollection<ReceitaViewModel> Get(int userId = 0);
        ReceitaViewModel Add(ReceitaViewModel model, int userId = 0);
        ReceitaViewModel Update(ReceitaViewModel model, int userId = 0);
        bool Delete(int receitaId);
    }
    public class ReceitaRepository : IReceitaRepository
    {

        public ReceitaViewModel? Get(int receitaId, int userId = 0)
        {
            using (var context = new ReceitasDeFamiliaDbContext())
            {
                return context.Receitas.Find(receitaId)
                    .ToReceitaViewModel(IsReceitaFavorita(receitaId, userId));
            }
        }

        public ICollection<ReceitaViewModel> Get(int userId = 0)
        {
            using (var context = new ReceitasDeFamiliaDbContext())
            {
                return context.Receitas.Where(x => !x.FoiDeletado).Select(x => x.ToReceitaViewModel(IsReceitaFavorita(x.IdReceita, userId))).ToList();
            }
        }

        public ReceitaViewModel Add(ReceitaViewModel model, int userId = 0)
        {
            using (var context = new ReceitasDeFamiliaDbContext())
            {
                var entity = model.ToReceitaEntity();
                context.Receitas.Add(entity);
                context.SaveChanges();
                return entity.ToReceitaViewModel(IsReceitaFavorita(entity.IdReceita, userId));
            }
        }

        public ReceitaViewModel Update(ReceitaViewModel model, int userId = 0)
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
                return receitaFromDB.ToReceitaViewModel(IsReceitaFavorita(receitaFromDB.IdReceita, userId));
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

        private static bool IsReceitaFavorita(int receitaId, int userId)
        {
            if(userId <= 0)
            {
                return false;
            }

            using (var context = new ReceitasDeFamiliaDbContext())
            {
                return context.Favoritos.Where(x => !x.FoiDeletado && x.IdReceita == receitaId && x.IdUsuario == userId).Any();
            }
        }
    }
}
