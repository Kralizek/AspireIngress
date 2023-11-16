using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace AspireIngress.Resources;

public class Project(string name)
{
    private readonly IList<Action<IHostApplicationBuilder>> _builderConfigurations = new List<Action<IHostApplicationBuilder>>();

    private readonly IList<Action<WebApplication>> _applicationConfiguration = new List<Action<WebApplication>>();

    public void AddHostApplicationBuilderConfiguration(Action<IHostApplicationBuilder> action)
    {
        _builderConfigurations.Add(action);
    }

    public void AddWebApplicationConfiguration(Action<WebApplication> action)
    {
        _applicationConfiguration.Add(action);
    }

    public Task RunAsync()
    {
        var options = new WebApplicationOptions
        {
            ApplicationName = name
        };
        
        var builder = WebApplication.CreateEmptyBuilder(options);

        foreach (var action in _builderConfigurations)
        {
            action(builder);
        }

        var app = builder.Build();

        foreach (var action in _applicationConfiguration)
        {
            action(app);
        }

        return app.RunAsync();
    }
}