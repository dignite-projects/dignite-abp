namespace Dignite.Abp.UserPoints;

public static class UserPointsErrorCodes
{
    public static class UserPoint
    {
        public const string UnsupportedPointType = "Abp:UserPoints:0001"; // The point type is not supported.
        public const string EntityNotPointable = "Abp:UserPoints:0002"; // The entity is not pointable.
        public const string InsufficientPoint = "Abp:UserPoints:0003"; // User has insufficient point for the operation.
        public const string UserPointTypeNotEnabled = "Abp:UserPoints:0004"; 
    }
}
