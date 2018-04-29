using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MavenGenerator.Scripts.Maven.Elements
{
    public class MavenBuild : MavenElement
    {
        public List<MavenElement> Elements { get; set; }
  
        public MavenBuild() 
            : base("build")
        {
            Elements = new List<MavenElement>();
        }

        public void AddElement(MavenElement element)
        {
            Elements.Add(element);
        }

        public override List<string> Interpret()
        {
            List<string> lines = new List<string>();
            lines.Add(CreateTabs() + Start);
            foreach (MavenElement element in Elements)
            {
                lines.AddRange(element.Interpret());
            }
            lines.Add(CreateTabs() + End);
            return lines;
        }
    }
}
