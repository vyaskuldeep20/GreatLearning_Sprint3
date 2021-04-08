using Microsoft.AspNetCore.Mvc;
using ProjectManager.Data.Core;
using ProjectManager.Models;

namespace ProjectManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : BaseController<Project, CoreProjectRepository>
    {
        public ProjectController(CoreProjectRepository repository) : base(repository)
        {

        }
    }
}
