using ProjectManager.Models;

namespace ProjectManager.Data.Core
{
    public class CoreUserRepository: CoreRepository<User,ApiDbContext>
    {
        public CoreUserRepository(ApiDbContext context) : base(context)
        {

        }
    }
}
