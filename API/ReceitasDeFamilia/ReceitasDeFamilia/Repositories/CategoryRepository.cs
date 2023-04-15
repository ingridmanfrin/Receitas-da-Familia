using ReceitasDeFamilia.Models;
using ReceitasDeFamilia.Repositories.Entities;
using ReceitasDeFamilia.Repositories.Extensions;

namespace ReceitasDeFamilia.Repositories
{
    public interface ICategoriaReceitaRepository
    {
        ICollection<CategoriaReceitaViewModel> Get();
    }

    public class CategoriaReceitaRepository : ICategoriaReceitaRepository
    {
        public ICollection<CategoriaReceitaViewModel> Get()
        {
            using (var context = new ReceitasDeFamiliaDbContext())
            {
                return context.CategoriasReceita.Where(x => !x.FoiDeletado).Select(x => x.ToCategoriaReceitaViewModel()).ToList();
            }
        }
    }
}
