using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using xamarinlab.Model.model;
using System.Text;

namespace xamarinlab.Services
{
    public class MembersService
    {
        private const string Url = "http://webapilab-tab.azurewebsites.net/";

        public async Task<List<Member>> GetAll()
        {
            var data = "";
            using (var client = new System.Net.Http.HttpClient())
            {
                var response = await client.GetAsync(Url + "api/members");
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    data = await response.Content.ReadAsStringAsync();
                }
            }
            return JsonConvert.DeserializeObject<List<Member>>(data);
        }
    }
}
