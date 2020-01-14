using RepositoryPattern.DAL;
using RepositoryPattern.Infrastructure.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern.Services
{
  public interface IUserService
  {
    List<User> GetAllUsers();
  }

  public class UserService : IUserService
  {
    private UserRepository userRepository;
    public UserService(TestEntities dbContext)
    {
      userRepository = new UserRepository(dbContext);
    }

    public List<User> GetAllUsers()
    {
      return userRepository.GetAll().ToList();
    }
  }
}
