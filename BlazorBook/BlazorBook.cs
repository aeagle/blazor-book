using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace BlazorBook
{
    public class StoryComponent : ComponentBase
    {
    }

    public class ComponentGroup
    {
        public string Name { get; set; }
        public List<ComponentMetadata> Components { get; set; } = new();
        public List<ComponentGroup> Children { get; set; } = new();
    }

    public static class Stories
    {
        static Dictionary<string, ComponentMetadata> _components = new();

        public static ComponentGroup Root { get; private set; } = new() { Name = "Root" };

        public static IEnumerable<ComponentMetadata> GetAll() => _components.Values;

        public static ComponentMetadata MetadataByName(string component) => _components[component];

        public static void Register(Type t)
        {
            var slug = t.FullName.Replace(".", "_").ToLower();
            var displayName = t.GetCustomAttributes(typeof(DisplayNameAttribute)).Cast<DisplayNameAttribute>().FirstOrDefault();

            var path = (displayName?.DisplayName ?? slug).Split("/".ToCharArray(), StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            var metadata = new ComponentMetadata
            {
                Slug = slug,
                Name = path.Last(),
                Type = t
            };
            _components.Add(slug, metadata);

            void fileComponent(ComponentGroup group, string[] path, ComponentMetadata componentMetadata)
            {
                if (path.Length == 1)
                {
                    group.Components.Add(componentMetadata);
                }
                else
                {
                    var childGroup = group.Children.FirstOrDefault(x => x.Name == path[0]);
                    if (childGroup == null)
                    {
                        childGroup = new ComponentGroup { Name = path[0] };
                        group.Children.Add(childGroup);
                    }

                    fileComponent(childGroup, path[1..], componentMetadata);
                }
            }

            fileComponent(Root, path, metadata);
        }

        public static void RegisterAllStories(this WebAssemblyHostBuilder host, params Assembly[] assemblies)
        {
            foreach (var assembly in assemblies)
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (type.IsSubclassOf(typeof(StoryComponent)))
                    {
                        Register(type);
                    }
                }
            }
        }

        public static IApplicationBuilder RegisterAllStories(this IApplicationBuilder app, params Assembly[] assemblies)
        {
            foreach (var assembly in assemblies)
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (type.IsSubclassOf(typeof(StoryComponent)))
                    {
                        Register(type);
                    }
                }
            }

            return app;
        }
    }

    public class ComponentMetadata
    {
        public string Slug { get; set; }
        public Type Type { get; set; }
        public string Name { get; set; }
    }
}