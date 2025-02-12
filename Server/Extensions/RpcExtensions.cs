using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MessagePack;
using OPP.Framework;
using OPP.Rpc.Messages;
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

public static class WebApplicationExtension
{
    public static void MapPostRpc(this WebApplication app, string pattern)
    {
        app.MapPost(pattern, async (HttpContext httpCtx, IRpcService rpcSvc) =>
        {
            var reqContentType = GetContentType(httpCtx.Request.Headers);
            var req = await LoadJsonStreamAsync<RpcRequest>(httpCtx.Request.Body); // TODO: 다른 ContentType

            if (req == null)
            {
                httpCtx.Response.StatusCode = StatusCodes.Status400BadRequest;
                await DumpAsync(httpCtx, reqContentType, new());
                return;
            }

            var res = rpcSvc.Invoke(httpCtx, req);
            await DumpAsync(httpCtx, reqContentType, res);
        });
    }


    private static string GetContentType(IHeaderDictionary inHeaderDict)
    {
        var contentType =  inHeaderDict["Content-Type"].ToString();
        if (string.IsNullOrEmpty(contentType))
        {
            return "application/json";
        }
        else
        {
            return contentType;
        }
    }

    private static async Task<T?> LoadJsonStreamAsync<T>(Stream inBody) where T : class
    {
        using (var reader = new StreamReader(inBody))
        {
            var str = await reader.ReadToEndAsync();
            return JsonSerializer.Deserialize<T>(str);
        }
    }

    private static async Task DumpAsync(HttpContext httpCtx, string inContentType, object objRes)
    {
        switch (inContentType)
        {
            case "application/json":
                await DumpJsonAsync(httpCtx, objRes);
                break;
            default:
                await DumpJsonAsync(httpCtx, objRes);
                break;
        }
    }

    private static async Task DumpJsonAsync(HttpContext httpCtx, object objRes)
    {
        var jsonRes = JsonSerializer.Serialize(objRes, JsonSerializerOpts);
        var bufRes = Encoding.UTF8.GetBytes(jsonRes);

        httpCtx.Response.ContentType = "application/json";
        await httpCtx.Response.Body.WriteAsync(bufRes);
    }

    private static async Task DumpMsgpLzBase64Async(HttpContext httpCtx, object objRes)
    {
        var bufRes = MessagePackSerializer.Serialize(objRes);
        var base64str = Convert.ToBase64String(bufRes);

        httpCtx.Response.ContentType = "text/plain";
        await httpCtx.Response.WriteAsync(base64str);
    }

    private readonly static JsonSerializerOptions JsonSerializerOpts = new JsonSerializerOptions()
    {
        PropertyNamingPolicy = null,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    };
}
