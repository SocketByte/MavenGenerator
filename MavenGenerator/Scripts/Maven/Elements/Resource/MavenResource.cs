using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MavenGenerator.Scripts.Maven.Elements
{
    public class MavenResource : MavenElement
    {
        public string Directory { get; set; }

        public MavenResource() 
            : base("resource", 3)
        {
            Directory = "src/main/resources";
        }

        public override List<string> Interpret()
        {
            List<string> lines = new List<string>
            {
                CreateTabs() + Start,
                CreateTabs(Tabs + 1) + "<directory>" + Directory + "</directory>",
                CreateTabs() + End
            };
            return lines;
        }
    }
}
