using Aspire.Hosting;
using Aspire.Hosting.ApplicationModel;

namespace AspireIngress.Resources;

public interface IIngressResource 
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
        where TDestination : IResource
    {
        return builder;
    }
}