using System;
using ProjectManager.Models;
using ProjectManager.Data.Core;

namespace ProjectManager.Data.Core
{
    public class CoreProjectRepository: CoreRepository<Project,ApiDbContext>
    {
        public CoreProjectRepository(ApiDbContext context) : base(context)
        {

        }
    }
}
