using ReceitasDeFamilia.Exceptions;
using ReceitasDeFamilia.Models;
using ReceitasDeFamilia.Repositories.Entities;
using ReceitasDeFamilia.Repositories.Extensions;

namespace ReceitasDeFamilia.Repositories
{
    public interface IFamiliaRepository
    {
        FamiliaViewModel? Get(int familiaId);
        ICollection<FamiliaViewModel> Get();
        FamiliaViewModel Add(FamiliaViewModel model);
        FamiliaViewModel Update(FamiliaViewModel model);
        bool Delete(int familiaId);
    }
    public class FamiliaRepository : IFamiliaRepository
    {

        public FamiliaViewModel? Get(int familiaId)
        {
            using (var context = new ReceitasDeFamiliaDbContext())
            {
                return context.Familias.Find(familiaId)
                    .ToFamiliaViewModel();
            }
        }

        public ICollection<FamiliaViewModel> Get()
        {
            using (var context = new ReceitasDeFamiliaDbContext())
            {
                return context.Familias.Where(x => !x.FoiDeletado).Select(x => x.ToFamiliaViewModel()).ToList();
            }
        }

        public FamiliaViewModel Add(FamiliaViewModel model)
        {
            using (var context = new ReceitasDeFamiliaDbContext())
            {
                var entity = model.ToFamiliaEntity();
                context.Familias.Add(entity);
                context.SaveChanges();
                return entity.ToFamiliaViewModel();
            }
        }

        public FamiliaViewModel Update(FamiliaViewModel model)
        {
            using (var context = new ReceitasDeFamiliaDbContext())
            {
                var familiaFromDB = context.Familias.Find(model.IdFamilia);
                if (familiaFromDB == null)
                {
                    return null;
                }
                familiaFromDB.UpdateFrom(model);
                context.Familias.Update(familiaFromDB);
                context.SaveChanges();
                return familiaFromDB.ToFamiliaViewModel();
            }
        }

        public bool Delete(int familiaId)
        {
            using (var context = new ReceitasDeFamiliaDbContext())
            {
                var familiaFromDB = context.Familias.Find(familiaId);
                if (familiaFromDB == null)
                {
                    return false;
                }

                familiaFromDB.FoiDeletado = true;

                context.Familias.Update(familiaFromDB);
                context.SaveChanges();
                return true;
            }
        }
    }
}
