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


        public async Task<MessageRespones> TextPostInfo(ContactsDTO contactsDTO)
        {
            var content = new StringContent(JsonConvert.SerializeObject(contactsDTO), Encoding.UTF8, "Application/json");
            var response = await _client.PostAsync(@"http://localhost:5000/v1/Contacts", content);
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<MessageRespones>(stringResponse);
            return result;
        }

        public async Task<MessageRespones> TextPutInfo(ContactsDTO contactsDTO, string id)
        {
            var content = new StringContent(JsonConvert.SerializeObject(contactsDTO), Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(@"http://localhost:5000/v1/Contacts/" + id, content);

            var stringResponse = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<MessageRespones>(stringResponse);
            return result;
        }

        public async Task<MessageRespones> TextDeleteInfo(string id)
        {
            var response = await _client.DeleteAsync(@"http://localhost:5000/v1/Contacts/"+id);
            var stringResponse = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<MessageRespones>(stringResponse);
            return result;
        }

        public async Task<IEnumerable<ContactsDTO>> TextGetAllInfo(int pageSize, int pageNumber)
        {
            var response = await _client.GetAsync(@"http://localhost:5000/v1/Contacts/?pageNumber=" + pageNumber + @"&pageSize=" + pageSize );
            var stringResponse = await response.Content.ReadAsStringAsync();
            Output.WriteLine(stringResponse);
            var result = JsonConvert.DeserializeObject<IEnumerable<ContactsDTO>>(stringResponse);
            return result;
        }

        public async Task<ContactsDTO> TextGetSingle(string id)
        {
            var response = await _client.GetAsync(@"http://localhost:5000/v1/Contacts/" + id);
            var stringResponse = await response.Content.ReadAsStringAsync();
            Output.WriteLine(stringResponse);
            var result = JsonConvert.DeserializeObject<ContactsDTO>(stringResponse);

            return result;
        }

        public async Task<IEnumerable<ContactsDTO>> TextGetByPropInfo(ContactsDTO contactsDTO)
        {
            var conent = new StringContent(JsonConvert.SerializeObject(contactsDTO), Encoding.UTF8, "appliccation/json");
            var response = await _client.PostAsync(@"http://localhost:5000/v1/Contacts/single", conent);
            var stringResonse = await response.Content.ReadAsStringAsync();
            Output.WriteLine(stringResonse);
            var result = JsonConvert.DeserializeObject<IEnumerable<ContactsDTO>>(stringResonse);
            return result;
        }
    }
}
