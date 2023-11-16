using AspireIngress.Resources;

var builder = DistributedApplication.CreateBuilder(args);

var api = builder.AddProject<Projects.AspireIngress_API>("api");

var web = builder.AddProject<Projects.AspireIngress_Web>("web");

builder.AddIngress()
    .WithServiceBinding(12000, "http")
    .WithPath("/api", api, preservePath: false)
    .WithPath("/", web, preservePath: true);

builder.Build().Run();
