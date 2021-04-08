using System;
using ProjectManager.Models;
using ProjectManager.Data.Core;

namespace ProjectManager.Data.Core
{
    public class CoreTaskRepository: CoreRepository<Task,ApiDbContext>
    {
        public CoreTaskRepository(ApiDbContext context) : base(context)
        {

        }
    }
}
