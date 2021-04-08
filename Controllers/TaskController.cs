using Microsoft.AspNetCore.Mvc;
using ProjectManager.Data.Core;
using Task = ProjectManager.Models.Task;

namespace ProjectManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : BaseController<Task, CoreTaskRepository>
    {
        public TaskController(CoreTaskRepository repository) : base(repository)
        {

        }
    }
}