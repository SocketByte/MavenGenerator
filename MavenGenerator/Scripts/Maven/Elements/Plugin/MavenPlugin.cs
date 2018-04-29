using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MavenGenerator.Scripts.Maven.Elements
{
    public class MavenPlugin : MavenElement
    {
        public string GroupId { get; set; }
        public string ArtifactId { get; set; }
        public string Version { get; set; }
        public List<CustomElement> CustomElements { get; set; }
  
        public MavenPlugin() 
            : base("plugin", 3)
        {
            CustomElements = new List<CustomElement>();
        }

        public void AddCustomElement(CustomElement element)
        {
            element.Tabs = Tabs + 1;
            CustomElements.Add(element);
        }

        public override List<string> Interpret()
        {
            List<string> lines = new List<string>();
            lines.Add(CreateTabs() + Start);
            lines.Add(CreateTabs(Tabs + 1) + "<groupId>" + GroupId + "</groupId>");
            lines.Add(CreateTabs(Tabs + 1) + "<artifactId>" + ArtifactId + "</artifactId>");
            lines.Add(CreateTabs(Tabs + 1) + "<version>" + Version + "</version>");
            foreach (CustomElement element in CustomElements)
            {
                lines.AddRange(element.Interpret());
            }
            lines.Add(CreateTabs() + End);
            return lines;
        }
    }
}
