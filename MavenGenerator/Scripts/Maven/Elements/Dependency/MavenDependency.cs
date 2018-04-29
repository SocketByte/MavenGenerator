using MavenGenerator.Scripts.Maven.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MavenGenerator.Scripts.Maven.Elements
{
    public class MavenDependency : MavenElement
    {
        public string GroupId { get; set; }
        public string ArtifactId { get; set; }
        public string Version { get; set; }
        public Scope? Scope { get; set; }

        public MavenDependency() : 
            base("dependency", 2)
        { }

        public override List<string> Interpret()
        {
            List<string> lines = new List<string>();
            lines.Add(CreateTabs() + Start);
            lines.Add(CreateTabs(Tabs + 1) + "<groupId>" + GroupId + "</groupId>");
            lines.Add(CreateTabs(Tabs + 1) + "<artifactId>" + ArtifactId + "</artifactId>");
            lines.Add(CreateTabs(Tabs + 1) + "<version>" + Version + "</version>");
            if (Scope != null)
            {
                lines.Add(CreateTabs(Tabs + 1) + "<scope>" + Scope.ToString().ToLower() + "</scope>");
            }
            lines.Add(CreateTabs() + End);
            return lines;
        }
    }
}
