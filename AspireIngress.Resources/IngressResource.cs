using Aspire.Hosting;
using Aspire.Hosting.ApplicationModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace AspireIngress.Resources;

public interface IIngressResource : IResourceWithBindings, IResourceWithEnvironment
{

}

public class IngressResource(string name) : Resource(name), IIngressResource
{
    
}

public static class IngressBuilderExtensions 
{
    public static IResourceBuilder<IngressResource> AddIngress(this IDistributedApplicationBuilder builder, string name = "default")
    {
        var resource = new IngressResource(name);

        return builder.AddResource(resource);
    }

    public static IResourceBuilder<IngressResource> WithPath<TDestination>(this IResourceBuilder<IngressResource> builder, string path, IResourceBuilder<TDestination> destination, bool preservePath = false)
        where TDestination : IResourceWithBindings
    {
        return builder.WithAnnotation(new ProxyPathAnnotation
        {
            Path = path,
            Destination = destination.Resource,
            PreservePath = preservePath
        });
    }

    public static IResourceBuilder<IngressResource> WithDefaultServices(this IResourceBuilder<IngressResource> builder, Func<IHostApplicationBuilder, IHostApplicationBuilder> defaultServices)
    {
        return builder;
    }

    public static IResourceBuilder<IngressResource> WithDefaultEndpoints(this IResourceBuilder<IngressResource> builder, Func<WebApplication, WebApplication> defaultEndpoints)
    {
        return builder;
    }
}

public class ProxyPathAnnotation : IResourceAnnotation
{
    public required string Path { get; set; }

    public required IResourceWithBindings Destination { get; set; }

    public bool PreservePath { get; set; }
}