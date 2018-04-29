using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MavenGenerator.Scripts.Maven.Elements
{
    public class MavenRepository : MavenElement
    {
        public string Id { get; set; }
        public string Url { get; set; }

        public MavenRepository() 
            : base("repository", 2)
        {
        }

        public override List<string> Interpret()
        {
            List<string> lines = new List<string>
            {
                CreateTabs() + Start,
                CreateTabs(Tabs + 1) + "<id>" + Id + "</id>",
                CreateTabs(Tabs + 1) + "<url>" + Url + "</url>",
                CreateTabs() + End
            };
            return lines;
        }
    }
}
