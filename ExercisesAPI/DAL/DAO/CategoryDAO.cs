using ExercisesAPI.DAL.DomainClasses;
using System.Collections.Generic;
using System.Linq;
namespace ExercisesAPI.DAL.DAO
{
    public class CategoryDAO
    {
        private AppDbContext _db;
        public CategoryDAO(AppDbContext ctx)
        {
            _db = ctx;
        }
        public List<Category> GetAll()
        {
            return _db.Categories.ToList<Category>();
        }
    }
}
