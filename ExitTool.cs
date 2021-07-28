using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ToolShed
{
    public class ExitTool : Tool
    {
        public ExitTool(HttpClient httpClient, IConfiguration configuration) : base(httpClient, configuration)
        {
        }

        public override string Name => "[red]Exit[/]";

        public override Task Run() => throw new NotImplementedException();
    }
}
