using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MavenGenerator.Scripts.Maven.Elements
{
    public class MavenDependencies : MavenElement
    {
        public List<MavenDependency> Dependencies { get; set; }

        public MavenDependencies()
            : base("dependencies")
        {
            Dependencies = new List<MavenDependency>();
        }

        public void AddDependency(MavenDependency dependency)
        {
            Dependencies.Add(dependency);
        }

        public override List<string> Interpret()
        {
            List<string> lines = new List<string>();
            lines.Add(CreateTabs() + Start);

            foreach (MavenDependency dependency in Dependencies)
                lines.AddRange(dependency.Interpret());

            lines.Add(CreateTabs() + End);
            return lines;
        }
    }
}
