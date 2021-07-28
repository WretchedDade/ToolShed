using Microsoft.Extensions.Configuration;
using Spectre.Console;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ToolShed.CerebroAzureDevOps
{
    public class CerebroAzureDevOpsTool : Tool
    {
        public CerebroAzureDevOpsTool(HttpClient httpClient, IConfiguration configuration) : base(httpClient, configuration)
        {
        }

        public override string Name => "Cerebor Capital Azure DevOps";

        public override async Task Run()
        {
            bool exit = false;

            do
            {
                AnsiConsole.Clear();
                AnsiConsole.Render(new FigletText("Dade's Tool Shed"));

                var tool = AnsiConsole.Prompt(new SelectionPrompt<SubTool>()
                    .Title("What would you like to do?")
                    .MoreChoicesText("[grey](Move up and down to reveal more tools)[/]")
                    .UseConverter(action => action.Name)
                    .AddChoices(GetSubTools(typeof(CerebroAzureDevOpsTool)))
                );

                if(tool is GoBackSubTool)
                    exit = true;
                else
                    await tool.Run();

            } while (!exit);
        }
    }
}
