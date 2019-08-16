using Chat.DataAccessLayer.DTO;
using Chat.DataAccessLayer.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.DataAccessLayer.Repository
{
    public class UserRepository
    {
        private readonly MicrosoftChatEntities _context;
        public UserRepository()
        {
            _context = new MicrosoftChatEntities();
        }
        public bool IsUserExist(string UserName)=> _context.users.Any(s => s.UserName == UserName);
         
        public void Add(UserDTO user)
        {
            _context.users.Add(new user
            {
                UserName = user.UserName,
                Password = user.Password
            });
            _context.SaveChanges();
        }

        public UserDTO GetUser(UserDTO user)
        {
            var entityuser = _context.users.FirstOrDefault(s => s.UserName == user.UserName && s.Password == user.Password);
            if (entityuser != null)
            {
                return new UserDTO()
                {
                    UserName = entityuser.UserName,
                    Password = entityuser.Password,
                    Id = entityuser.id
                };
            }
            return null;
        }

        public List<UserDTO> GetAllUsers(int CurrentUserId)
        {
            var query = (from user in _context.users
                         where user.id!=CurrentUserId
                         select new UserDTO()
                         {
                             Id = user.id,
                             UserName = user.UserName

                         });

            var data = query.ToList();

            return data;
        }

    }
}
