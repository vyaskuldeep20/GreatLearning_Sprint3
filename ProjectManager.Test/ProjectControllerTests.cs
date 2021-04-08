using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ProjectManager.Models;
using Xunit;

namespace ProjectManager.Test
{
    public class ProjectControllerTests: IClassFixture<ProjectManagerTestFactory>
    {
        private readonly HttpClient _client;

        public ProjectControllerTests(ProjectManagerTestFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async System.Threading.Tasks.Task Get_All_Projects_Response_OK()
        {
            var expectedProjects = new List<Project>();
            expectedProjects.Add(new Project()
            {
                Id = 1,
                Name = "Project 1",
                Detail = "To be Added",
                CreatedOn = DateTime.Now
            });
            var response = await _client.GetAsync("/api/Project");
            response.EnsureSuccessStatusCode();
            Assert.NotNull(response);
            var responseString = await response.Content.ReadAsStringAsync();
            var actualProjects = JsonConvert.DeserializeObject <List<Project>> (responseString);
            Assert.True(actualProjects.Any());
            Assert.Equal(actualProjects,expectedProjects);
        }

        [Fact]
        public async System.Threading.Tasks.Task Get_Project_by_id_and_response_ok()
        {
            var response = await _client.GetAsync("/api/Project/1");
            response.EnsureSuccessStatusCode();
            Assert.NotNull(response);
            var responseString = await response.Content.ReadAsStringAsync();
            var Project = JsonConvert.DeserializeObject < Project > (responseString);
            Assert.True(Project.Id == 1);
        }

        [Fact]
        public async System.Threading.Tasks.Task Post_create_new_Project_and_response_ok()
        {
            var newProject = new Project()
            {
                Id = 22,
                Name = "Project 22"
            };
            var response = await _client.PostAsync("/api/Project",
                new StringContent(JsonConvert.SerializeObject(newProject), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            Assert.True(response.StatusCode == HttpStatusCode.Created);
        }


        [Fact]
        public async System.Threading.Tasks.Task Put_modify_Project_and_response_ok()
        {
            var ProjectResponse = await _client.GetAsync("/api/Project/1");
            var responseString = await ProjectResponse.Content.ReadAsStringAsync();
            var Project = JsonConvert.DeserializeObject<Project>(responseString);
            Project.Name = "Project 1";
            var putResponse = await _client.PutAsJsonAsync<Project>("/api/Project", Project);
            putResponse.EnsureSuccessStatusCode();
            var putResponseString = await putResponse.Content.ReadAsStringAsync();
            var updatedProject = JsonConvert.DeserializeObject<Project>(putResponseString);
            Assert.True(updatedProject.Name.Equals("Project 1"));
        }

        [Fact]
        public async System.Threading.Tasks.Task Delete_Project_and_response_no_content()
        {
            var response = await _client.DeleteAsync($"api/Project/1");
            Assert.True(response.StatusCode == HttpStatusCode.NoContent);
        }
    }
}
