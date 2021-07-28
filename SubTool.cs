using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace ToolShed
{
    public abstract class SubTool
    {
        protected readonly HttpClient httpClient;
        protected readonly IConfiguration configuration;

        protected SubTool(HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;
            this.configuration = configuration;
        }

        public abstract string Name { get; }
        public abstract Task Run();
    }
}
