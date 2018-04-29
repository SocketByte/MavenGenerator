using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MavenGenerator.Scripts.Maven.Elements
{
    public class MavenProperty : MavenElement
    {
        public MavenProperty(string name, string value)
            : base(name, 2)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }
        public string Value { get; set; }

        public override List<string> Interpret()
        {
            List<string> lines = new List<string>
            {
                CreateTabs() + Start + Value + End
            };

            return lines;
        }
    }
}
