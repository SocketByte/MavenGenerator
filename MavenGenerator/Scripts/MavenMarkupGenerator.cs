using MavenGenerator.Models;
using MavenGenerator.Models.Data;
using MavenGenerator.Scripts.Maven;
using MavenGenerator.Scripts.Maven.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MavenGenerator.Scripts
{
    public class MavenMarkupGenerator
    {
        public static IReadOnlyList<string> Create(MavenGeneratorViewModel model)
        {
            MavenBuilder builder = new MavenBuilder
            {
                GroupId = model.GroupId,
                ArtifactId = model.ArtifactId,
                Version = model.Version,
                Packaging = "jar"
            };
            builder.AddDefaults();

            MavenProperties properties = new MavenProperties();

            properties.AddProperty(new MavenProperty("java.version", model.JavaVersion));
            if (model.MainClass != null)
            {
                properties.AddProperty(new MavenProperty("main.class", model.MainClass));
            }

            if (model.DeployUrl != null)
            {
                MavenDistributionManagement distributionManagement = new MavenDistributionManagement();
                MavenRepository repository = new MavenRepository
                {
                    Id = "deploy-id",
                    Url = model.DeployUrl
                };

                distributionManagement.AddRepository(repository);
                builder.PushElement(distributionManagement);
            }

            properties.AddProperty(new MavenProperty("project.build.sourceEncoding", "UTF-8"));

            MavenBuild build = new MavenBuild();
            MavenRepositories repositories = new MavenRepositories();
            MavenDependencies dependencies = new MavenDependencies();
            MavenPlugins plugins = new MavenPlugins();
            if (model.Language == Language.Kotlin)
            {
                build.AddElement(new MavenProperty("sourceDirectory", "src/main/kotlin"));

                properties.AddProperty(new MavenProperty("kotlin.version", "1.2.41"));
                // Enables experimental kotlin compiler
                properties.AddProperty(new MavenProperty("kotlin.compiler.incremental", "true"));

                MavenDependency kotlinDependency = new MavenDependency
                {
                    GroupId = "org.jetbrains.kotlin",
                    ArtifactId = "kotlin-stdlib",
                    Version = "${kotlin.version}",
                    Scope = Maven.Data.Scope.Compile
                };
                dependencies.AddDependency(kotlinDependency);

                MavenPlugin kotlinPlugin = new MavenPlugin
                {
                    GroupId = "org.jetbrains.kotlin",
                    ArtifactId = "kotlin-maven-plugin",
                    Version = "${kotlin.version}"
                };
                CustomElement configuration = new CustomElement(3);
                configuration.AddLine("<executions>");
                configuration.AddLine(2, "<execution>");
                configuration.AddLine(3, "<id>compile</id>");
                configuration.AddLine(3, "<goals><goal>compile</goal></goals>");
                configuration.AddLine(3, "<configuration>");
                configuration.AddLine(4, "<sourceDirs>");
                configuration.AddLine(5, "<sourceDir>${project.basedir}/src/main/kotlin</sourceDir>");
                configuration.AddLine(5, "<sourceDir>${project.basedir}/src/main/java</sourceDir>");
                configuration.AddLine(4, "</sourceDirs>");
                configuration.AddLine(3, "</configuration>");
                configuration.AddLine(2, "</execution>");
                configuration.AddLine("</executions>");
                kotlinPlugin.AddCustomElement(configuration);

                plugins.AddPlugin(kotlinPlugin);
            }

            if (model.IsBukkitPlugin)
            {
                properties.AddProperty(new MavenProperty("bukkit.version", "1.12.2-R0.1-SNAPSHOT"));

                MavenRepository bukkitRepository = new MavenRepository()
                {
                    Id = "bukkit-repo",
                    Url = "https://hub.spigotmc.org/nexus/content/groups/public/"
                };
                repositories.AddRepository(bukkitRepository);
                
                MavenDependency bukkitDependency = new MavenDependency
                {
                    GroupId = "org.bukkit",
                    ArtifactId = "bukkit",
                    Version = "${bukkit.version}",
                    Scope = Maven.Data.Scope.Provided
                };
                dependencies.AddDependency(bukkitDependency);
            }
            builder.PushElement(properties);
            builder.PushElement(repositories);
            if (model.IsBukkitPlugin)
            {
                builder.PushElement(dependencies);
            }

            foreach (Plugin plugin in model.Plugins)
            {
                switch (plugin)
                {
                    case Plugin.Compiler:
                        {
                            MavenPlugin compilerPlugin = new MavenPlugin
                            {
                                GroupId = "org.apache.maven.plugins",
                                ArtifactId = "maven-compiler-plugin",
                                Version = "3.7.0"
                            };
                            CustomElement configuration = new CustomElement(3);
                            configuration.AddLine("<configuration>");
                            configuration.AddLine(2, "<source>${java.version}</source>");
                            configuration.AddLine(2, "<target>${java.version}</target>");
                            configuration.AddLine("</configuration>");
                            compilerPlugin.AddCustomElement(configuration);

                            if (model.Language == Language.Kotlin)
                            {
                                CustomElement executions = new CustomElement(3);
                                executions.AddLine("<executions>");
                                executions.AddLine(2, "<execution>");
                                executions.AddLine(3, "<id>default-compile</id>");
                                executions.AddLine(3, "<phase>none</phase>");
                                executions.AddLine(2, "</execution>");
                                executions.AddLine("</executions>");

                                executions.AddLine("<executions>");
                                executions.AddLine(2, "<execution>");
                                executions.AddLine(3, "<id>java-compile</id>");
                                executions.AddLine(3, "<phase>compile</phase>");
                                executions.AddLine(3, "<goals>");
                                executions.AddLine(4, "<goal>compile</goal>");
                                executions.AddLine(3, "</goals>");
                                executions.AddLine(2, "</execution>");
                                executions.AddLine("</executions>");

                                compilerPlugin.AddCustomElement(executions);
                            }
                            plugins.AddPlugin(compilerPlugin);
                            break;
                        }
                    case Plugin.Shade: {
                            MavenPlugin shadePlugin = new MavenPlugin
                            {
                                GroupId = "org.apache.maven.plugins",
                                ArtifactId = "maven-shade-plugin",
                                Version = "3.1.1"
                            };

                            CustomElement executions = new CustomElement(3);
                            executions.AddLine("<executions>");
                            executions.AddLine(2, "<execution>");
                            executions.AddLine(3, "<phase>package</phase>");
                            executions.AddLine(3, "<goals>");
                            executions.AddLine(4, "<goal>shade</goal>");
                            executions.AddLine(3, "</goals>");
                            executions.AddLine(2, "</execution>");
                            executions.AddLine("</executions>");
                            shadePlugin.AddCustomElement(executions);
                            plugins.AddPlugin(shadePlugin);
                            break;
                        }
                    case Plugin.Jar:
                        {
                            MavenPlugin jarPlugin = new MavenPlugin
                            {
                                GroupId = "org.apache.maven.plugins",
                                ArtifactId = "maven-jar-plugin",
                                Version = "3.1.0"
                            };

                            if (model.MainClass != null)
                            {
                                CustomElement configuration = new CustomElement(3);
                                configuration.AddLine("<configuration>");
                                configuration.AddLine(2, "<archive>");
                                configuration.AddLine(3, "<manifest>");
                                configuration.AddLine(4, "<addClasspath>true</addClasspath>");
                                configuration.AddLine(4, "<mainClass>${main.class}</mainClass>");
                                configuration.AddLine(3, "</manifest>");
                                configuration.AddLine(2, "</archive>");
                                configuration.AddLine("</configuration>");
                                jarPlugin.AddCustomElement(configuration);
                            }
                            plugins.AddPlugin(jarPlugin);
                            break;
                        }
                    case Plugin.Assembly:
                        {
                            MavenPlugin jarPlugin = new MavenPlugin
                            {
                                GroupId = "org.apache.maven.plugins",
                                ArtifactId = "maven-assembly-plugin",
                                Version = "3.1.0"
                            };
                            CustomElement configuration = new CustomElement(3);
                            configuration.AddLine("<configuration>");
                            if (model.MainClass != null)
                            {
                                configuration.AddLine(2, "<archive>");
                                configuration.AddLine(3, "<manifest>");
                                configuration.AddLine(4, "<addClasspath>true</addClasspath>");
                                configuration.AddLine(4, "<mainClass>${main.class}</mainClass>");
                                configuration.AddLine(3, "</manifest>");
                                configuration.AddLine(2, "</archive>");
                            }
                            configuration.AddLine(2, "<descriptorRefs>");
                            configuration.AddLine(3, "<descriptorRef>jar-with-dependencies</descriptorRef>");
                            configuration.AddLine(2, "</descriptorRefs>");
                            configuration.AddLine("</configuration>");

                            jarPlugin.AddCustomElement(configuration);
                            plugins.AddPlugin(jarPlugin);
                            break;
                        }
                }
            }

            build.AddElement(plugins);

            MavenResources resources = new MavenResources();
            MavenResource resource = new MavenResource(); // Default: src/main/resources
            resources.AddResource(resource);
            build.AddElement(resources);

            builder.PushElement(build);

            return builder.GetGeneratedLines();
        }
    }
}
