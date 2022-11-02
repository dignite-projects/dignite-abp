using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace Dignite.FileExplorer;

public static class CommonOperations
{
    public static OperationAuthorizationRequirement Create = new OperationAuthorizationRequirement { Name = nameof(Create) };
    public static OperationAuthorizationRequirement Update = new OperationAuthorizationRequirement { Name = nameof(Update) };
    public static OperationAuthorizationRequirement Delete = new OperationAuthorizationRequirement { Name = nameof(Delete) };
    public static OperationAuthorizationRequirement Get = new OperationAuthorizationRequirement { Name = nameof(Get) };
}