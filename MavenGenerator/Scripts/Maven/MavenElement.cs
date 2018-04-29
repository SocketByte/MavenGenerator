using MavenGenerator.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MavenGenerator.Scripts.Maven
{
    public abstract class MavenElement
    {
        public string Start { get; set; }
        public string End { get; set; }
        public int Tabs { get; set; }

        public List<MavenElement> Content { get; set; }

        public MavenElement(string name, int tabs)
        {
            Start = "<" + name + ">";
            End = "</" + name + ">";
            Tabs = tabs;
            Content = new List<MavenElement>();
        }

        public MavenElement(string name)
        {
            Start = "<" + name + ">";
            End = "</" + name + ">";
            Tabs = 1;
            Content = new List<MavenElement>();
        }

        public string CreateTabs()
        {
            return CreateTabs(Tabs);
        }

        public string CreateTabs(int tabs)
        {
            return "    ".Repeat(tabs);
        }

        public abstract List<string> Interpret();
    }
}
