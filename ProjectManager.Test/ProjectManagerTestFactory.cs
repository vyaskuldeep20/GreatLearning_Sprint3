using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectManager.Models;

namespace ProjectManager.Test
{
    
    public class ProjectManagerTestFactory : WebApplicationFactory<Startup>
    {
       
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseStartup < Startup > ();
            builder.UseContentRoot(Directory.GetCurrentDirectory());
            builder.UseUrls("http://localhost:5000");
            builder.ConfigureAppConfiguration((builderContext, config) => { config.AddJsonFile("appsettings.json"); });
            builder.ConfigureServices((context, services) =>
            {
                
                services.AddDbContext < ApiDbContext > (options => options.UseInMemoryDatabase(""));
                //Build the service provider
                var sp = services.BuildServiceProvider();

                //create a scope to obtain a reference to database context
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var dbcontext = scopedServices.GetRequiredService < ApiDbContext > ();
                    try
                    {
                        // Ensure the database is created.
                        dbcontext.Database.EnsureCreated();
                        AddUsers(dbcontext);
                        AddProjects(dbcontext);
                        AddTasks(dbcontext);

                        dbcontext.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred creating database. Error: {ex.Message}");
                    }
                }
            });
        }
        public static void AddUsers(ApiDbContext context)
        {
            context.Users.AddRange(
            new User() { Id = 1, Email = "User1@gmail.com", FirstName = "User", LastName = "One", Password = "User123" });
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
