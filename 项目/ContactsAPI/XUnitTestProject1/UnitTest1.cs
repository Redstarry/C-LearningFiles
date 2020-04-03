using ContactsAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using XUnitTestProject1.Func;

namespace XUnitTestProject1
{
    public class UnitTest1
    {
        protected readonly ITestOutputHelper Output;
        private readonly Funca funca;
        public UnitTest1(ITestOutputHelper testOutput)
        {
            Output = testOutput;
            funca = new Funca(testOutput);
        }
        #region
        //HttpClient _client;
        //public UnitTest1()
        //{
        //    _client = base.GetClient();
        //}
        //[Theory]
        //[InlineData("Contacts")]
        //public async Task TextGetName(string Controller)
        //{
        //    var response = await _client.GetAsync($"http://localhost:5000/v1/Contacts/5facbfd3-706e-4cf8-8ea8-294f629cbbda");
        //    response.EnsureSuccessStatusCode();
        //    var stringResponse = await response.Content.ReadAsStringAsync();
        //    var result = JsonConvert.DeserializeObject<ContactsAPI.Models.Contacts>(stringResponse);

        //    Assert.Equal("tp", result.Name);
        //}

        //[Theory]
        //[InlineData("Contacts")]


        //[Fact]
        //public async Task TextPsotInfo()
        //{
        //    List<ContactsDTO> data = new List<ContactsDTO>() {
        //        new ContactsDTO(){ 
        //            Name = "",
        //            Phone = "",
        //            IdCard = ""
        //        },
        //        new ContactsDTO(){
        //            Name = "",
        //            Phone = "15362359845",
        //            IdCard = "321456987741258963"
        //        },
        //        new ContactsDTO(){
        //            Name = "",
        //            Phone = "15362359845",
        //            IdCard = "321456987741258"
        //        },
        //        new ContactsDTO(){
        //            Name = "sdf",
        //            Phone = "15362359846",
        //            IdCard = "32145698774125896X"
        //        },
        //        new ContactsDTO(){
        //            Name = "sdf",
        //            Phone = "15362359846",
        //            IdCard = "32145698774125896x"
        //        },

        //    };
        //    //var Data = new ContactsDTO();
        //    //Data.Name = "";
        //    //Data.Phone = "";
        //    //Data.IdCard = "";
        //    foreach (var item in data)
        //    {
        //        var content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "Application/json");
        //        var response = await _client.PostAsync($"http://localhost:5000/v1/Contacts", content);
        //        var stringResponse = await response.Content.ReadAsStringAsync();
        //        var result = JsonConvert.DeserializeObject<MessageRespones>(stringResponse);
        //        Assert.Equal(1, result.Stat);
        //    }
        //}
        #endregion
        [Theory]
        [InlineData("zzz", "15396324581", "789654023258041")]
        [InlineData("zzz", "15396324582", "789654123208745412")]
        [InlineData("zzz", "15396324581", "789654123258042413")]
        [InlineData("zzz", "15396324581", "78965412320874141x")]
        public async Task PostInfoSuceess(string name,string phone,string idcard)
        {
            var data = new ContactsDTO() { 
            
               Name = name,
               Phone = phone,
               IdCard = idcard
            };
            //var exPostInfo = new Funca();
            var result =  await funca.TextPostInfo(data);
            Output.WriteLine(result.Mes);
            Assert.Equal(1, result.Stat);
        }
        [Theory]
        [InlineData("111111111111111111111111", "15396324581", "78965412325874142x")]
        [InlineData("", "", "")]
        public async Task PostInfoError(string name, string phone, string idcard)
        {
            var data = new ContactsDTO()
            {

                Name = name,
                Phone = phone,
                IdCard = idcard
            };
            //var exPostInfo = new Funca();
            var result = await funca.TextPostInfo(data);
            Output.WriteLine(result.Mes);
            Assert.Equal(-1, result.Stat);
        }

        /// <summary>
        /// 更新数据的时候，当id存在的情况下。
        /// </summary>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        /// <param name="idcard"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [Theory]
        [InlineData("zzz2", "15396324581", "", "1f7fe819-38b5-44b0-8556-6c1294a2d46d")]
        [InlineData("zzz2", "", "", "1f7fe819-38b5-44b0-8556-6c1294a2d46d")]
        public async Task PutInfoSuceess(string name ,string phone, string idcard, string id)
        {
            var data = new ContactsDTO()
            {
                Name = name,
                Phone = phone,
                IdCard = idcard
            };
            //var exPutInfo = new Funca();
            var result = await funca.TextPutInfo(data, id);
            Output.WriteLine(result.Mes);
            Assert.Equal(1, result.Stat);
        }

        /// <summary>
        /// 更新数据的时候，当id不存在的情况下。
        /// </summary>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        /// <param name="idcard"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [Theory]
        [InlineData("zzz2", "15396324581", "", "3a093f22-cded-41f4-af9a-4fdd7a0ecb59")]
        [InlineData("zzz2", "", "", "3a093f22-cded-41f4-af9a-4fdd7a0ecb59")]
        [InlineData("", "", "", "3a093f22-cded-41f4-af9a-4fdd7a0ecb59")]
        public async Task PutInfoError(string name, string phone, string idcard, string id)
        {
            var data = new ContactsDTO()
            {
                Name = name,
                Phone = phone,
                IdCard = idcard
            };
            //var exPutInfo = new Funca();
            var result = await funca.TextPutInfo(data, id);
            Output.WriteLine(result.Mes);
            Assert.Equal(-1, result.Stat);
        }


        /// <summary>
        /// 删除数据的时候，当id存在的情况下。
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Theory]
        [InlineData("5facbfd3-706e-4cf8-8ea8-294f629cbbda")]
        public async Task DeleteInfoSuceess(string id)
        {
            //var exDeleteInfo = new Funca();
            var result = await funca.TextDeleteInfo(id);
            Output.WriteLine(result.Mes);
            Assert.Equal(1, result.Stat);
        }

        /// <summary>
        /// 删除数据的时候，当id不存在的情况下。
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Theory]
        [InlineData("3a093f22-cded-41f4-af9a-4fdd7a0ecb59")]
        public async Task DeleteInfoError(string id)
        {
            //var exDeleteInfo = new Funca();
            var result = await funca.TextDeleteInfo(id);
            Output.WriteLine(result.Mes);
            Assert.Equal(-1, result.Stat);
        }


        /// <summary>
        /// 获取全部的数据，分页成功,正常的页码和 一页的数据是否在20
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        [Theory]
        [InlineData(20, 1)]
        [InlineData(19, 1)]
        [InlineData(18, 1)]
        [InlineData(17, 1)]
        [InlineData(16, 1)]
        [InlineData(15, 1)]
        [InlineData(14, 1)]
        [InlineData(13, 1)]
        [InlineData(12, 1)]
        [InlineData(11, 1)]
        [InlineData(10, 1)]
        [InlineData(9, 1)]
        [InlineData(8, 1)]
        [InlineData(7, 1)]
        [InlineData(6, 1)]
        [InlineData(5,1)]
        [InlineData(4,1)]
        [InlineData(3,1)]
        [InlineData(2,1)]
        [InlineData(1,1)]
        public async Task GetAllInfoSuceess(int pageSize, int pageNumber)
        {
            var result = await funca.TextGetAllInfo(pageSize, pageNumber);
            Assert.NotEmpty(result);
        }


        /// <summary>
        /// 获取全部的数据，分页成功
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        [Theory]
        [InlineData(21, 2)]
        public async Task GetAllInfoError(int pageSize, int pageNumber)
        {
            var result = await funca.TextGetAllInfo(pageSize, pageNumber);
            Assert.Empty(result);
        }


        /// <summary>
        /// 根据id获取信息，当id存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Theory]
        [InlineData("1f7fe819-38b5-44b0-8556-6c1294a2d46d")]
        public async Task GetSingleSuceess(string id)
        {
            var result = await funca.TextGetSingle(id);
            Assert.NotNull(result);
        }

        /// <summary>
        /// 根据id获取信息，当id不存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Theory]
        [InlineData("3a093f22-cded-41f4-af9a-4fdd7a0ecb59")]
        public async Task GetSingleError(string id)
        {
            var result = await funca.TextGetSingle(id);
            Assert.Null(result);
        }

        /// <summary>
        /// 通过其他信息来获取信息，成功的测试
        /// </summary>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        /// <param name="idcard"></param>
        /// <returns></returns>
        [Theory]
        [InlineData("zzz", "", "")]
        [InlineData("", "15396324581", "")]
        [InlineData("", "", "612301199105180419")]
        [InlineData("qwe", "15385421698", "963258741321456987")]
        public async Task GetByPropInfoSuceess(string name ,string phone, string idcard)
        {
            var data = new ContactsDTO()
            {
                Name = name,
                Phone = phone,
                IdCard = idcard
            };
            var result = await funca.TextGetByPropInfo(data);
            
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// 通过其他信息来获取信息，失败的测试
        /// </summary>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        /// <param name="idcard"></param>
        /// <returns></returns>
        [Theory]
        [InlineData("", "", "")]
        [InlineData("444", "15390417715", "963258741321456987")]
        public async Task GetByPropInfoError(string name, string phone, string idcard)
        {
            var data = new ContactsDTO()
            {
                Name = name,
                Phone = phone,
                IdCard = idcard
            };
            var result = await funca.TextGetByPropInfo(data);

            Assert.Empty(result);
        }
    }

}
