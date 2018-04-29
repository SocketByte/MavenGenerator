using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MavenGenerator.Scripts.Maven.Elements
{
    public class MavenPlugins : MavenElement
    {
        public List<MavenPlugin> Plugins { get; set; }

        public MavenPlugins() 
            : base("plugins", 2)
        {
            Plugins = new List<MavenPlugin>();
        }

        public void AddPlugin(MavenPlugin plugin)
        {
            Plugins.Add(plugin);
        }

        public override List<string> Interpret()
        {
            List<string> lines = new List<string>();
            lines.Add(CreateTabs() + Start);
            foreach (MavenPlugin plugin in Plugins)
            {
                lines.AddRange(plugin.Interpret());
            }
            lines.Add(CreateTabs() + End);
            return lines;
        }
    }
}
