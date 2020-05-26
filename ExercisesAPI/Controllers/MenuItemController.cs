using System.Collections.Generic;
using ExercisesAPI.DAL;
using ExercisesAPI.DAL.DAO;
using ExercisesAPI.DAL.DomainClasses;
using Microsoft.AspNetCore.Mvc;
namespace ExercisesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemController : ControllerBase
    {
        AppDbContext _db;
        public MenuItemController(AppDbContext context)
        {
            _db = context;
        }
        [Route("{catid}")]
        public ActionResult<List<MenuItem>> Index(int catid)
        {
            MenuItemDAO dao = new MenuItemDAO(_db);
            List<MenuItem> itemsForCategory = dao.GetAllByCategory(catid);
            return itemsForCategory;
        }
    }
}
