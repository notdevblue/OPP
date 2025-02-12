using System.Text.Json;
using OPP.Rpc.Messages;

namespace OPP.Framework;

public class RpcHandler<SVC, REQ, RES> : IRpcHandler
    where SVC : class
    where REQ : class, new()
    where RES : class, new()
{
    public delegate RES OnInvokeDelegate(SVC svc, REQ req);

    public RpcHandler(string inMethod, OnInvokeDelegate inOnInvoke)
    {
        Method = inMethod;
        OnInvoke = inOnInvoke;
    }

    public RpcResponse Invoke(HttpContext httpCtx, RpcRequest req)
    {
        var svc = httpCtx.RequestServices.GetService<SVC>();
        if (svc == null)
        {
            return RpcResponse.From(req).BindError("NOT_FOUND_SERVICE", () => new { typeof(SVC).Name });
        }
        
        var payload = req.Payload;
        if (payload == null)
        {
            payload = "{}";
        }

        var reqDeserialized = JsonSerializer.Deserialize<REQ>(payload.ToString()!);
        if (reqDeserialized == null)
        {
            reqDeserialized = new REQ();
        }
        
        var res = OnInvoke(svc, reqDeserialized);
        return RpcResponse.From(req).AddPayload(res!);
    }

    public string Method { get; }
    public OnInvokeDelegate OnInvoke { get; }
}