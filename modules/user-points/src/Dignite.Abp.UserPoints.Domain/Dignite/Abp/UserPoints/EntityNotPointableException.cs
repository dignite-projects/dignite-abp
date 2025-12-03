using Volo.Abp;

namespace Dignite.Abp.UserPoints;

public class EntityNotPointableException : BusinessException
{
    public EntityNotPointableException(string entityType)
    {
        Code = UserPointsErrorCodes.UserPoint.EntityNotPointable;
        EntityType = entityType;
        WithData(nameof(EntityType), EntityType);
    }

    public string EntityType { get; }
}
