using Grpc.Core;

namespace HavenoSharp.Extensions;

public static class RpcExceptionExtensions
{
    public static string GetErrorMessage(this RpcException rpcException)
    {
        return rpcException.Message.Split("Detail=\"")[1].TrimEnd("\")").ToString();
    }
}
