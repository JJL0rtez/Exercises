using System.Collections.Generic;
using ExercisesAPI.DAL;
using ExercisesAPI.DAL.DAO;
using ExercisesAPI.DAL.DomainClasses;
using Microsoft.AspNetCore.Mvc;
namespace ExercisesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        AppDbContext _db;
        public CategoryController(AppDbContext context)
        {
            _db = context;
        }
        public ActionResult<List<Category>> Index()
        {
            CategoryDAO dao = new CategoryDAO(_db);
            List<Category> allCategories = dao.GetAll();
            return allCategories;
        }
    }
}