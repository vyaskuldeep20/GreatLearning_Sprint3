using Microsoft.AspNetCore.Mvc;
using ProjectManager.Data.Core;
using ProjectManager.Models;

namespace ProjectManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController<User, CoreUserRepository>
    {
        public UserController(CoreUserRepository repository) : base(repository)
        {

        }
    }
}