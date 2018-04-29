using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MavenGenerator.Scripts.Maven.Elements
{
    public class CustomElement : MavenElement
    {
        public List<string> Lines { get; set; }

        public CustomElement(int tabs) 
            : base("customElement", tabs)
        {
            Lines = new List<string>();
        }

        public CustomElement()
            : base("customElement", 1)
        {
            Lines = new List<string>();
        }

        public void AddLine(int tabs, string line)
        {
            Lines.Add(CreateTabs(Tabs + tabs) + line);
        }

        public void AddLine(string line)
        {
            Lines.Add(CreateTabs(Tabs + 1) + line);
        }

        public override List<string> Interpret()
        {
            return Lines;
        }
    }
}
