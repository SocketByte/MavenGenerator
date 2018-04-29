using MavenGenerator.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MavenGenerator.Models
{
    public class MavenGeneratorViewModel
    {
        public Language Language { get; set; }

        public string ArtifactId { get; set; }
        public string GroupId { get; set; }
        public string Version { get; set; }
        public string JavaVersion { get; set; }
        public string MainClass { get; set; }
        public string DeployUrl { get; set; }
        
        public bool IsBukkitPlugin { get; set; }

        public int PluginAmount { get; set; }
        public List<Plugin> Plugins { get; set; }
    }
}
