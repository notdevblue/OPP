namespace OPP.Rpc.Messages;

public class ReqHelloWorld_Greet
{
    public string Name { get; set; } = string.Empty;
}

public class ResHelloWorld_Greet
{
    public string Message { get; set; } = string.Empty;
}