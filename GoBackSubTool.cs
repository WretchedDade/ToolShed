using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ToolShed
{
    public class GoBackSubTool : SubTool
    {
        public GoBackSubTool(HttpClient httpClient, IConfiguration configuration) : base(httpClient, configuration)
        {
        }

        public override string Name => "Go Back";

        public override Task Run() => throw new NotImplementedException();
    }
}
