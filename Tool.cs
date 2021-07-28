using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ToolShed
{
    public abstract class Tool
    {
        protected readonly HttpClient httpClient;
        protected readonly IConfiguration configuration;

        protected Tool(HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;
            this.configuration = configuration;
        }

        public abstract string Name { get; }
        public abstract Task Run();

        public List<SubTool> GetSubTools(Type parentToolType)
        {
            var subTools = parentToolType.Assembly.GetTypes()
                .Where(type => type.Namespace == parentToolType.Namespace && type.IsSubclassOf(typeof(SubTool)) && !type.IsAbstract)
                .Select(type => (Activator.CreateInstance(type, httpClient, configuration) as SubTool)!)
                .ToList();

            subTools.Add(new GoBackSubTool(httpClient, configuration));

            return subTools;    
        }
    }
}
