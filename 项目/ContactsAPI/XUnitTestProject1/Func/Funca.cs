using ContactsAPI.Models;
using ContactsAPI.Models.PageModel;
using Newtonsoft.Json;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace XUnitTestProject1.Func
{
    public class Funca : ApiTestBase ,IFunc
    {
        HttpClient _client;
        protected readonly ITestOutputHelper Output;
        public Funca(ITestOutputHelper testOutput)
        {
            Output = testOutput;
            _client = base.GetClient();
        }


        public async Task<ResultDTO> TextPostInfo(ContactsDTO contactsDTO)
        {
            var content = new StringContent(JsonConvert.SerializeObject(contactsDTO), Encoding.UTF8, "Application/json");
            var response = await _client.PostAsync(@"http://localhost:5000/v1/Contacts", content);
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResultDTO>(stringResponse);
            return result;
        }

        public async Task<ResultDTO> TextPutInfo(ContactsDTO contactsDTO, string id)
        {
            var content = new StringContent(JsonConvert.SerializeObject(contactsDTO), Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(@"http://localhost:5000/v1/Contacts/" + id, content);

            var stringResponse = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ResultDTO>(stringResponse);
            return result;
        }

        public async Task<ResultDTO> TextDeleteInfo(string id)
        {
            var response = await _client.DeleteAsync(@"http://localhost:5000/v1/Contacts/"+id);
            var stringResponse = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ResultDTO>(stringResponse);
            return result;
        }

        public async Task<ResultDTO> TextGetAllInfo(int pageSize, int pageNumber)
        {
            var response = await _client.GetAsync(@"http://localhost:5000/v1/Contacts/?pageNumber=" + pageNumber + @"&pageSize=" + pageSize );
            var stringResponse = await response.Content.ReadAsStringAsync();
            Output.WriteLine(stringResponse);
            var result = JsonConvert.DeserializeObject<ResultDTO>(stringResponse);
            return result;
        }

        public async Task<ResultDTO> TextGetSingle(string id)
        {
            var response = await _client.GetAsync(@"http://localhost:5000/v1/Contacts/" + id);
            var stringResponse = await response.Content.ReadAsStringAsync();
            Output.WriteLine(stringResponse);
            var result = JsonConvert.DeserializeObject<ResultDTO>(stringResponse);

            return result;
        }

        public async Task<ResultDTO> TextGetByPropInfo(ContactsDTO contactsDTO)
        {
            var conent = new StringContent(JsonConvert.SerializeObject(contactsDTO), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(@"http://localhost:5000/v1/Contacts/propselect", conent);
            var stringResonse = await response.Content.ReadAsStringAsync();
            Output.WriteLine(stringResonse);
            var result = JsonConvert.DeserializeObject<ResultDTO>(stringResonse);
            return result;
        }
    }
}
