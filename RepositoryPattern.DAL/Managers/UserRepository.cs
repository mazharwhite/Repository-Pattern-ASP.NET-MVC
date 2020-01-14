using RepositoryPattern.DAL;
using RepositoryPattern.Infrastructure.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern.DAL
{
  public class UserRepository : DbRepository<User>
  {
    public UserRepository(TestEntities context) : base(context)
    {
    }

    // DAL Functions Go Here
  }
}
