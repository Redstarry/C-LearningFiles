using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTestProject1
{
    public class UnitTest1 : ApiTestBase
    {
        HttpClient _client;
        public UnitTest1()
        {
            _client = base.GetClient();
        }
        [Theory]
        [InlineData("Contacts")]
        public async Task TextGetName(string controllerName)
        {
            var response = await _client.GetAsync($"http://localhost:5000/v1/Contacts/5facbfd3-706e-4cf8-8ea8-294f629cbbda");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ContactsAPI.Models.Contacts>(stringResponse);

            Assert.Equal("tp", result.Name);
        }

        [Fact]
        public void Test1()
        {

        }
    }
}
