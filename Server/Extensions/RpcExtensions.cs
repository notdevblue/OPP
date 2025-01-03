using OPP.Framework;
using OPP.Rpc.Services;

namespace OPP.Extension;

public static class WebApplicationBuilderExtension
{
    public static void InitializeRpc(this WebApplicationBuilder builder, List<IRpcHandler> handlerList)
    {
        builder.Services.AddSingleton<IRpcService>(provider => 
            new RpcSerivce(provider.GetRequiredService<ILogger<RpcSerivce>>(), handlerList));
    }
}