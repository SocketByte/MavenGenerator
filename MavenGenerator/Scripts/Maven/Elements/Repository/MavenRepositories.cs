using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MavenGenerator.Scripts.Maven.Elements
{
    public class MavenRepositories : MavenElement
    {
        public List<MavenRepository> Repositories { get; set; }

        public MavenRepositories() 
            : base("repositories")
        {
            Repositories = new List<MavenRepository>();
        }

        public void AddRepository(MavenRepository repository)
        {
            Repositories.Add(repository);
        }

        public override List<string> Interpret()
        {
            List<string> lines = new List<string>();
            lines.Add(CreateTabs() + Start);
            foreach (MavenRepository repository in Repositories)
            {
                lines.AddRange(repository.Interpret());
            }
            lines.Add(CreateTabs() + End);
            return lines;
        }
    }
}
