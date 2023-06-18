using MoviesAPI.Data;
using MoviesAPI.Models;
using MoviesAPI.Repository.IRepository;

namespace MoviesAPI.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _bd;

        public CategoryRepository(ApplicationDbContext bd)
        {
            _bd = bd;
        }
        public bool CreateCategory(Category category)
        {
            category.InitialDate = DateTime.Now;
            _bd.Categories.Add(category);
            return Save();
        }

        public bool DeleteCategory(Category category)
        {
            _bd.Categories.Remove(category);
            return Save();
        }

        public ICollection<Category> GetCategories()
        {
            return _bd.Categories.OrderBy(c => c.Name).ToList();
        }

        public Category GetCategory(int id)
        {
            return _bd.Categories.FirstOrDefault(c => c.Id == id);
        }

        public bool IsCategoryExists(string name)
        {
            bool result = _bd.Categories.Any(c => c.Name.ToLower().Trim() == name.ToLower().Trim());
            return result;
        }

        public bool IsCategoryExists(int id)
        {
            return _bd.Categories.Any(c => c.Id == id);
        }

        public bool Save()
        {
            return _bd.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateCategory(Category category)
        {
            category.InitialDate = DateTime.Now;
            _bd.Categories.Update(category);
            return Save();
        }
    }
}
