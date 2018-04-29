using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MavenGenerator.Scripts.Maven.Elements
{
    public class MavenResources : MavenElement
    {
        public List<MavenResource> Resources { get; set; }

        public MavenResources() 
            : base("resources", 2)
        {
            Resources = new List<MavenResource>();
        }

        public void AddResource(MavenResource resource)
        {
            Resources.Add(resource);
        }

        public override List<string> Interpret()
        {
            List<string> lines = new List<string>();
            lines.Add(CreateTabs() + Start);
            foreach (MavenResource resource in Resources)
            {
                lines.AddRange(resource.Interpret());
            }
            lines.Add(CreateTabs() + End);
            return lines;
        }
    }
}
