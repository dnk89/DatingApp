using System.Collections.Generic;
using System.Threading.Tasks;
using API.Entities;

namespace API.Data
{
    public interface IUsersRepository
    {
         Task<IEnumerable<AppUser>> GetUsers();

         Task<AppUser> GetUser(int id);

         Task<AppUser> GetUser(string username);

         Task AddUser(AppUser user);
    }
}