using System.Collections.Generic;
using System.Linq;
using ExercisesAPI.DAL.DomainClasses;
namespace ExercisesAPI.DAL.DAO
{
    public class MenuItemDAO
    {
        private AppDbContext _db;
        public MenuItemDAO(AppDbContext ctx)
        {
            _db = ctx;
        }
        public List<MenuItem> GetAllByCategory(int id)
        {
            return _db.MenuItems.Where(item => item.Category.Id == id).ToList();
        }
    }
}