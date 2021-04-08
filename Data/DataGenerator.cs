using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProjectManager.Models;

namespace ProjectManager.Data
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApiDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApiDbContext>>()))
            {
                AddUsers(context);
                AddProjects(context);
                AddTasks(context);

                context.SaveChanges();
            }
        }
        public static void AddUsers(ApiDbContext context)
        {
            context.Users.AddRange(
            new User() { Id = 1, Email = "User1@gmail.com", FirstName = "User", LastName = "One", Password = "User123" },
            new User() { Id = 2, Email = "User2@gmail.com", FirstName = "User", LastName = "Two", Password = "User234" });
        }
        public static void AddProjects(ApiDbContext context)
        {
            context.Projects.AddRange(
            new Project()
            {
                Id = 1,
                Name = "Project 1",
                Detail = "To be Added",
                CreatedOn = DateTime.Now
            });
        }
        public static void AddTasks(ApiDbContext context)
        {
            context.Tasks.AddRange(
             new Task()
             {
                 Id = 1,
                 Status = 1,
                 AssignedToUserId = 1,
                 ProjectId = 1,
                 Detail = "To be Added",
                 CreatedOn = DateTime.Now
             });
        }
    }
}

