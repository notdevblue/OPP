using OPP.Extension;
using OPP.Framework;
using OPP.Rpc.Messages;
using OPP.Rpc.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<HelloWorldRpcService>();

// Add services to the container.
builder.InitializeRpc([
    new RpcHandler<HelloWorldRpcService, ReqHelloWorld_Greet, ResHelloWorld_Greet>("hello-world.greet", (svc, req) => svc.Greet(req))
]);

var app = builder.Build();

app.MapPostRpc("/rpc");

app.Run();
