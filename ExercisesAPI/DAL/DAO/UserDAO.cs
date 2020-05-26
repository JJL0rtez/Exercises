using ExercisesAPI.DAL.DomainClasses;
using System.Linq;
namespace ExercisesAPI.DAL.DAO
{
    public class UserDAO
    {
        private AppDbContext _db;
        public UserDAO(AppDbContext ctx)
        {
            _db = ctx;
        }
        public User Register(User user)
        {
            _db.Users.Add(user);
            _db.SaveChanges();
            return user;
        }
        public User GetByEmail(string email)
        {
            User user = _db.Users.FirstOrDefault(u => u.Email == email);
            return user;
        }
    }
}
