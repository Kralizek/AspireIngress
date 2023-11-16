using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Yarp.ReverseProxy.Configuration;

namespace AspireIngress.Resources;

/// <summary>
/// This is just a sketch file to try to wire up a web project that uses YARP.
/// </summary>
public class Sketch
{
    public async Task DoSomethingAsync(IList<ProxyPathAnnotation> paths)
    {
        var routes = CreateRouteConfigs(paths);

        var clusters = CreateClusterConfigs(paths);
        
        var project = new Project("default");

        project.AddHostApplicationBuilderConfiguration(builder => builder.Services.AddReverseProxy()
            .LoadFromMemory(routes, clusters));
        
        project.AddWebApplicationConfiguration(app => app.MapReverseProxy());

        await project.RunAsync().ConfigureAwait(false);
    }

    private static IReadOnlyList<ClusterConfig> CreateClusterConfigs(IList<ProxyPathAnnotation> paths)
    {
        // var pathsByDestination = paths.ToLookup(path => path.Destination.Name);
        //
        // pathsByDestination.Select(destination => new ClusterConfig
        // {
        //     ClusterId = destination.Key,
        //     Destinations = new Dictionary<string, DestinationConfig>
        //     {
        //         ["default"] = new DestinationConfig
        //         {
        //             Address = destination.First().Destination.GetEndpoint(destination.First().Destination.Annotations.OfType<ServiceBindingAnnotation>().First().)
        //         }
        //     }
        // })
        
        return Array.Empty<ClusterConfig>();
    }

    private static IReadOnlyList<RouteConfig> CreateRouteConfigs(IList<ProxyPathAnnotation> paths)
    {
        return Array.Empty<RouteConfig>();
    }
}