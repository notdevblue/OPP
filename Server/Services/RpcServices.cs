using System.Collections.Concurrent;
using OPP.Framework;
using OPP.Rpc.Messages;

namespace OPP.Rpc.Services;

public class RpcSerivce : IRpcService
{
    public RpcSerivce(ILogger<RpcSerivce> logger, IEnumerable<IRpcHandler> handlerList)
    {
        _logger = logger;
        _handlerList = handlerList;

        foreach (var eachHandler in _handlerList)
        {
            _logger.LogDebug($"* rpc_method({eachHandler.Method})");
            if (!_implDict.TryAdd(eachHandler.Method, eachHandler))
            {
                _logger.LogError($"NOT_ADDED_METHOD({eachHandler.Method})");
                continue;
            }
        }
    }

    public RpcResponse Invoke(HttpContext httpCtx, RpcRequest req)
    {
        if (!_implDict.TryGetValue(req.Method, out var rpcImpl))
        {
            return RpcResponse.From(req).BindError("NOT_FOUND_METHOD", () => new { req.Method });
        }

        return rpcImpl.Invoke(httpCtx, req);
    }

    public IEnumerable<IRpcHandler> HandlerList => _handlerList;
    private readonly IEnumerable<IRpcHandler> _handlerList;
    private readonly ILogger<RpcSerivce> _logger;
    private readonly ConcurrentDictionary<string, IRpcHandler> _implDict = [];
}