namespace Dignite.Abp.UserPoints;

public static class UserPointsErrorCodes
{
    public static class UserPoint
    {
        public const string EntityNotPointable = "Abp:UserPoints:0002"; // The entity is not pointable.
        public const string InsufficientPoint = "Abp:UserPoints:0003"; // User has insufficient point for the operation.
    }
}
