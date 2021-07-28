using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Spectre.Console;
using System;
using System.Linq;
using System.Net.Http;

namespace ToolShed
{
    public class Program
    {
        public static readonly HttpClient HttpClient = new();

        public static IConfigurationRoot Configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .AddUserSecrets<Program>()
            .Build();

        private static async Task Main(string[] args)
        {
            try
            {
                var tools = typeof(Tool).Assembly.GetTypes()
                    .Where(type => type != typeof(ExitTool) && type.IsSubclassOf(typeof(Tool)) && !type.IsAbstract)
                    .Select(type => (Activator.CreateInstance(type, HttpClient, Configuration) as Tool)!)
                    .ToList();

                tools.Add(new ExitTool(HttpClient, Configuration));

                bool exit = false;

                do
                {
                    AnsiConsole.Clear();
                    AnsiConsole.Render(new FigletText("Dade's Tool Shed"));

                    var tool = AnsiConsole.Prompt(new SelectionPrompt<Tool>()
                        .Title("What tool would you like to use?")
                        .MoreChoicesText("[grey](Move up and down to reveal more tools)[/]")
                        .UseConverter(tool => tool.Name)
                        .AddChoices(tools)
                    );

                    if (tool is ExitTool)
                        exit = true;
                    else
                        await tool.Run();

                } while (!exit);

                AnsiConsole.Clear();
            }
            catch(Exception ex)
            {
                AnsiConsole.WriteException(ex);
            }

        }
    }
}
