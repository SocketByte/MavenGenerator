using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MavenGenerator.Scripts.Maven
{
    public class MavenBuilder
    {
        private readonly List<string> lines = new List<string>();
        private readonly List<MavenElement> elements = new List<MavenElement>();

        public string GroupId { get; set; }
        public string ArtifactId { get; set; }
        public string Version { get; set; }
        public string Packaging { get; set; }

        public void AddDefaults()
        {
            lines.Add("<project xmlns=\"http://maven.apache.org/POM/4.0.0\"");
            lines.Add("    xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"");
            lines.Add("    xsi:schemaLocation=\"http://maven.apache.org/POM/4.0.0 http://maven.apache.org/xsd/maven-4.0.0.xsd\">");

            lines.Add("");
            lines.Add("    <modelVersion>4.0.0</modelVersion>");
            lines.Add("");
            lines.Add("    <groupId>" + GroupId + "</groupId>");
            lines.Add("    <artifactId>" + ArtifactId + "</artifactId>");
            lines.Add("    <version>" + Version + "</version>");
            if (Packaging != null)
            {
                lines.Add("    <packaging>" + Packaging + "</packaging>");
            }
        }

        public void PushElement(MavenElement element)
        {
            elements.Add(element);

            lines.AddRange(element.Interpret());
        }

        public IReadOnlyList<string> GetGeneratedLines()
        {
            lines.Add("</project>");
            return lines.AsReadOnly();
        }

        public IReadOnlyList<MavenElement> GetElements()
        {
            return elements.AsReadOnly();
        }
    }
}
