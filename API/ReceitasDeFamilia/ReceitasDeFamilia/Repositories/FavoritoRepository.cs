using Microsoft.EntityFrameworkCore;
using ReceitasDeFamilia.Exceptions;
using ReceitasDeFamilia.Models;
using ReceitasDeFamilia.Repositories.Entities;
using ReceitasDeFamilia.Repositories.Extensions;

namespace ReceitasDeFamilia.Repositories
{
    public interface IFavoritoRepository
    {
        ICollection<ReceitaViewModel> Get(int usuarioId);
        ReceitaViewModel Add(FavoritoViewModel model);
        bool Delete(FavoritoViewModel model);
    }
    public class FavoritoRepository : IFavoritoRepository
    {
        public ICollection<ReceitaViewModel> Get(int usuarioId)
        {
            using (var context = new ReceitasDeFamiliaDbContext())
            {
                return context.Favoritos.Include(x => x.Receita).Where(x => !x.FoiDeletado && x.IdUsuario == usuarioId).Select(x => x.Receita.ToReceitaViewModel(true)).ToList();
            }
        }

        public ReceitaViewModel Add(FavoritoViewModel model)
        {
            using (var context = new ReceitasDeFamiliaDbContext())
            {
                var receita = context.Receitas.Where(x => x.IdReceita == model.IdReceita).FirstOrDefault();

                if (receita == null)
                {
                    throw new ReceitaNotFoundException();
                }

                var favoritoExiste = context.Favoritos.Where(x => x.IdReceita == model.IdReceita && x.IdUsuario == model.IdUsuario).FirstOrDefault();

                if (favoritoExiste == null)
                {
                    var entity = model.ToFavoritoEntity();
                    context.Favoritos.Add(entity);
                }
                else
                {
                    favoritoExiste.FoiDeletado = false;
                    favoritoExiste.DataAlteracao = DateTime.Now;
                    context.Favoritos.Update(favoritoExiste);
                }

                context.SaveChanges();

                return receita.ToReceitaViewModel(true);
            }
        }

        public bool Delete(FavoritoViewModel model)
        {
            using (var context = new ReceitasDeFamiliaDbContext())
            {
                var favoritoFromDB = context.Favoritos.Where(x => x.IdReceita == model.IdReceita && x.IdUsuario == model.IdUsuario).FirstOrDefault();
                if (favoritoFromDB == null)
                {
                    return false;
                }

                favoritoFromDB.FoiDeletado = true;
                favoritoFromDB.DataAlteracao = DateTime.Now;

                context.Favoritos.Update(favoritoFromDB);
                context.SaveChanges();
                return true;
            }
        }
    }
}
