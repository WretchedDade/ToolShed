using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ToolShed.CerebroAzureDevOps
{
    public class CerebroAzureDevOpsService
    {
        private readonly HttpClient httpClient;

        private const string PROJECT = "Cerebro Capital Platform";
        private const string TEAM = "Cerebro Capital Platform Team";

        public CerebroAzureDevOpsService(HttpClient httpClient, IConfiguration configuration)
        {
            httpClient.BaseAddress = new Uri("https://cerebrocapital.visualstudio.com");

            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format("{0}:{1}", "", configuration["Cerebro:DevOpsToken"])));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

            this.httpClient = httpClient;   
        }

        public async Task<List<object>> GetIterations()
        {
            var response = await httpClient.GetAsync($"{PROJECT}/{TEAM}/_apis/work/teamsettings/iterations?api-version=6.0");

            if (response.IsSuccessStatusCode)
            {
                var test = await response.Content.ReadAsStringAsync();
            }

            return new();
        }
    }
}
