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

    public static class Stories
    {
        static Dictionary<string, ComponentMetadata> _components = new();

        public static IEnumerable<ComponentMetadata> GetAll() => _components.Values;

        public static ComponentMetadata MetadataByName(string component) => _components[component];

        public static void Register(Type t)
        {
            var slug = t.FullName.Replace(".", "_").ToLower();
            var displayName = t.GetCustomAttributes(typeof(DisplayNameAttribute)).Cast<DisplayNameAttribute>().FirstOrDefault();
            _components.Add(
                slug, 
                new ComponentMetadata
                {
                    Slug = slug,
                    Name = displayName?.DisplayName ?? slug,
                    Type = t
                }
            );
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