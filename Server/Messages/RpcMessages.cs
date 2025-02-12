using System.Text.Json.Serialization;
using Microsoft.VisualBasic;

namespace OPP.Rpc.Messages;

public class RpcRequest
{
    public ulong Seq { get; set; } = 0;
    public string Method { get; set; } = string.Empty;
    public object? Payload { get; set; } = null;
}

public class RpcError
{
    public static RpcError From(string inMsg, object inParam)
    {
        var outErr = new RpcError()
        {
            Msg = inMsg,
            Hash = inMsg, // TODO: 해쉬 작업
            Parameter = inParam
        };
        return outErr;
    }

    public string Hash { get; private set; } = string.Empty;
    public string Msg { get; private set; } = string.Empty;
    public object Parameter { get; set; } = null!;
}

public class RpcResponse
{
    public static RpcResponse From(RpcRequest req)
    {
        var outRes = new RpcResponse()
        {
            Seq = req.Seq,
            Method = req.Method,
        };
        return outRes;
    }

    public RpcResponse BindError(string inMsg, Func<object> inParam)
    {
        Error = RpcError.From(inMsg, inParam());
        return this;
    }

    public RpcResponse AddPayload(object inPayload)
    {
        Payload = inPayload;
        return this;
    }

    public ulong Seq { get; set; }
    public string Method { get; set; } = string.Empty;
    public object? Payload { get; set; } = null;
    public RpcError? Error { get; set; } = null;
}
