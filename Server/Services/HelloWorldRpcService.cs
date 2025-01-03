using OPP.Rpc.Messages;

namespace OPP.Rpc.Services;

public class HelloWorldRpcService
{
    public HelloWorldRpcService(
        ILogger<HelloWorldRpcService> logger)
    {
        _logger = logger;
    }

    public ResHelloWorld_Greet Greet(ReqHelloWorld_Greet req)
    {
        var msg = $"Hello {req.Name}!";

        _logger.LogInformation(msg);

        return new ResHelloWorld_Greet()
        {
            Message=msg
        };
    }


    private readonly ILogger<HelloWorldRpcService> _logger;
}