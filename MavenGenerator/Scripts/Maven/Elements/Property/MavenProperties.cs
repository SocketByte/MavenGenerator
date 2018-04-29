using MavenGenerator.Scripts.Maven.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MavenGenerator.Scripts.Maven.Elements
{
    public class MavenProperties : MavenElement
    {
        public List<MavenProperty> Properties { get; set; }

        public MavenProperties() 
            : base("properties")
        {
            Properties = new List<MavenProperty>();
        }

        public void AddProperty(MavenProperty property)
        {
            Properties.Add(property);
        }

        public override List<string> Interpret()
        {
            List<string> lines = new List<string>();
            lines.Add(CreateTabs() + Start);
            foreach (MavenProperty property in Properties)
            {
                lines.AddRange(property.Interpret());
            }
            lines.Add(CreateTabs() + End);
            return lines;
        }
    }
}
