using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace ToolShed.CerebroAzureDevOps
{
    public class ViewIterationsSubTool : SubTool
    {
        private readonly CerebroAzureDevOpsService azureDevOps;

        public ViewIterationsSubTool(HttpClient httpClient, IConfiguration configuration) : base(httpClient, configuration)
        {
            azureDevOps = new(httpClient, configuration);
        }

        public override string Name => "View Iterations";

        public override async Task Run()
        {
            _ = await azureDevOps.GetIterations();
        }
    }
}
