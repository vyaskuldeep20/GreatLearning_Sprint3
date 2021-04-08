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
using Task = ProjectManager.Models.Task;

namespace ProjectManager.Test
{
    public class TaskControllerTests: IClassFixture<ProjectManagerTestFactory>
    {
        private readonly HttpClient _client;

        public TaskControllerTests(ProjectManagerTestFactory factory)
        {
            _client = factory.CreateClient();
        }
        [Fact]
        public async System.Threading.Tasks.Task Get_All_Tasks_Response_OK()
        {
            var expectedTasks = new List<Task>();
            expectedTasks.Add(new Task()
            {
                Id = 1,
                Status = 1,
                AssignedToUserId = 1,
                ProjectId = 1,
                Detail = "To be Added",
                CreatedOn = DateTime.Now
            });
            var response = await _client.GetAsync("/api/Task");
            response.EnsureSuccessStatusCode();
            Assert.NotNull(response);
            var responseString = await response.Content.ReadAsStringAsync();
            var actualTasks = JsonConvert.DeserializeObject <List<Task>> (responseString);
            Assert.True(actualTasks.Any());
            Assert.Equal(actualTasks,expectedTasks);
        }

        [Fact]
        public async System.Threading.Tasks.Task Get_Task_by_id_and_response_ok()
        {
            var response = await _client.GetAsync("/api/Task/1");
            response.EnsureSuccessStatusCode();
            Assert.NotNull(response);
            var responseString = await response.Content.ReadAsStringAsync();
            var Task = JsonConvert.DeserializeObject < Task > (responseString);
            Assert.True(Task.Id == 1);
        }

        [Fact]
        public async System.Threading.Tasks.Task Post_create_new_Task_and_response_ok()
        {
            var newTask = new Task()
            {
                Id = 12,
                Status = 1,
            };
            var response = await _client.PostAsync("/api/Task",
                new StringContent(JsonConvert.SerializeObject(newTask), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            Assert.True(response.StatusCode == HttpStatusCode.Created);
        }


        [Fact]
        public async System.Threading.Tasks.Task Put_modify_Task_and_response_ok()
        {
            var TaskResponse = await _client.GetAsync("/api/Task/1");
            var responseString = await TaskResponse.Content.ReadAsStringAsync();
            var Task = JsonConvert.DeserializeObject<Task>(responseString);
            Task.Detail = "inprogress";
            var putResponse = await _client.PutAsJsonAsync<Task>("/api/Task", Task);
            putResponse.EnsureSuccessStatusCode();
            var putResponseString = await putResponse.Content.ReadAsStringAsync();
            var updatedTask = JsonConvert.DeserializeObject<Task>(putResponseString);
            Assert.True(updatedTask.Detail.Equals("inprogress"));
        }

        [Fact]
        public async System.Threading.Tasks.Task Delete_Task_and_response_no_content()
        {
            var response = await _client.DeleteAsync($"api/Task/1");
            Assert.True(response.StatusCode == HttpStatusCode.NoContent);
        }
    }
}
