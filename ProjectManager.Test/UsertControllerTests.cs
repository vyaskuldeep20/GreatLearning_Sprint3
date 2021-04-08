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
    public class UserControllerTests: IClassFixture<ProjectManagerTestFactory>
    {
        private readonly HttpClient _client;

        public UserControllerTests(ProjectManagerTestFactory factory)
        {
            _client = factory.CreateClient();
        }
        [Fact]
        public async System.Threading.Tasks.Task Get_All_Users_Response_OK()
        {
            var expectedUsers = new List<User>();
            expectedUsers.Add(new User() { Id = 1, Email = "User1@gmail.com", FirstName = "User", LastName = "One", Password = "User123" });
            var response = await _client.GetAsync("/api/user");
            response.EnsureSuccessStatusCode();
            Assert.NotNull(response);
            var responseString = await response.Content.ReadAsStringAsync();
            var actualUsers = JsonConvert.DeserializeObject <List<User>> (responseString);
            Assert.True(actualUsers.Any());
            Assert.Equal(actualUsers,expectedUsers);
        }

        [Fact]
        public async System.Threading.Tasks.Task Get_user_by_id_and_response_ok()
        {
            var response = await _client.GetAsync("/api/user/1");
            response.EnsureSuccessStatusCode();
            Assert.NotNull(response);
            var responseString = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject < User > (responseString);
            Assert.True(user.Id == 1);
        }

        [Fact]
        public async System.Threading.Tasks.Task Post_create_new_user_and_response_ok()
        {
            var newuser = new User()
            {
                Id = 55,
                FirstName = "ram",
                LastName = "kumar"
            };
            var response = await _client.PostAsync("/api/user",
                new StringContent(JsonConvert.SerializeObject(newuser), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            Assert.True(response.StatusCode == HttpStatusCode.Created);
        }


        [Fact]
        public async System.Threading.Tasks.Task Put_modify_user_and_response_ok()
        {
            var userResponse = await _client.GetAsync("/api/user/1");
            var responseString = await userResponse.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<User>(responseString);
            user.FirstName = "Ram";
            var putResponse = await _client.PutAsJsonAsync<User>("/api/user", user);
            putResponse.EnsureSuccessStatusCode();
            var putResponseString = await putResponse.Content.ReadAsStringAsync();
            var updateduser = JsonConvert.DeserializeObject<User>(putResponseString);
            Assert.True(updateduser.FirstName.Equals("Ram"));
        }

        [Fact]
        public async System.Threading.Tasks.Task Delete_user_and_response_no_content()
        {
            var response = await _client.DeleteAsync($"api/user/1");
            Assert.True(response.StatusCode == HttpStatusCode.NoContent);
        }
    }
}
