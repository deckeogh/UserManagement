using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Models.Repository;

namespace UserManagement.Models.DataManager
{
    public class UserManager : IDataRepository<User>
    {
        readonly UserContext _userContext;
        public UserManager(UserContext context)
        {
            _userContext = context;
        }
        public void Add(User entity)
        {
            _userContext.Users.Add(entity);
            _userContext.SaveChanges();
        }

        public void Delete(User entity)
        {
            _userContext.Users.Remove(entity);
            _userContext.SaveChanges();
        }

        public User Get(long id)
        {
            return _userContext.Users.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<User> GetAll()
        {
            return _userContext.Users.ToList();
        }

        public void Update(User user, User entity)
        {
            user.Name     = entity.Name;
            user.Surnames = entity.Surnames;
            user.Email    = entity.Email;
            user.Password = entity.Password;
            user.Country  = entity.Country;
            user.Phone    = entity.Phone;
            user.PostCode = entity.PostCode;
        }
    }
}
