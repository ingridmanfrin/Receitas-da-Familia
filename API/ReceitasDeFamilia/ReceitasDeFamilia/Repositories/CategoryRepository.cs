using ReceitasDeFamilia.Models;
using ReceitasDeFamilia.Repositories.Entities;
using ReceitasDeFamilia.Repositories.Extensions;

namespace ReceitasDeFamilia.Repositories
{
    public interface ICategoryRepository
    {
        CategoryViewModel? Get(int categoryId);
        CategoryViewModel Add(CategoryViewModel model);
        CategoryViewModel Update(CategoryViewModel model);
        bool Delete(int categoryId);
    }

    public class CategoryRepository : ICategoryRepository
    {
        public CategoryViewModel Add(CategoryViewModel model)
        {
            throw new NotImplementedException();
            //using (var context = new ReceitasDeFamiliaDbContext())
            //{
            //    var entity = model.ToCategoryEntity();
            //    context.Categories.Add(entity);
            //    context.SaveChanges();
            //    return entity.ToCategoryViewModel();
            //}
        }
        public CategoryViewModel? Get(int categoryId)
        {
            throw new NotImplementedException();
            //using (var context = new ReceitasDeFamiliaDbContext())
            //{
            //    return context.Categories.Find(categoryId).ToCategoryViewModel();
            //}
        }

        public CategoryViewModel Update(CategoryViewModel model)
        {
            throw new NotImplementedException();
            //using (var context = new ReceitasDeFamiliaDbContext())
            //    {
            //        var CategoryFromDB = context.Categories.Find(model.CategoryId);
            //        if (CategoryFromDB == null)
            //        {
            //            return null;
            //        }
            //        CategoryFromDB.UpdateFrom(model);
            //        context.Categories.Update(CategoryFromDB);
            //        context.SaveChanges();
            //        return CategoryFromDB.ToCategoryViewModel();
            //    }
        }
        public bool Delete(int categoryId)
        {
            throw new NotImplementedException();
            //using (var context = new ReceitasDeFamiliaDbContext())
            //{
            //    var categoryFromDB = context.Categories.Find(categoryId);
            //    if (categoryFromDB == null)
            //    {
            //        return false;
            //    }

            //    categoryFromDB.IsDeleted = true;

            //    context.Categories.Update(categoryFromDB);
            //    context.SaveChanges();
            //    return true;
            //}
        }
    }
}
