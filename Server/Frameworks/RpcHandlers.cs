using OPP.Rpc.Messages;

namespace OPP.Framework;

public class RpcHandler<SVC, REQ, RES> : IRpcHandler
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

        var res = OnInvoke(svc, (REQ)req.Paramter!);
        return RpcResponse.From(req).AddPayload(res!);
    }

    public string Method { get; }
    public OnInvokeDelegate OnInvoke { get; }
}