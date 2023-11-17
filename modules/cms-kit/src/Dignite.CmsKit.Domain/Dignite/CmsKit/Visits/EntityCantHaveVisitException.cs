﻿using JetBrains.Annotations;
using System.Runtime.Serialization;
using Volo.Abp;

namespace Dignite.CmsKit.Visits;

public class EntityCantHaveVisitException : BusinessException
{
    public EntityCantHaveVisitException(SerializationInfo serializationInfo, StreamingContext context) : base(serializationInfo, context)
    {
    }

    public EntityCantHaveVisitException([NotNull] string entityType)
    {
        Code = CmsKitErrorCodes.Visits.EntityCantHaveVisit;
        EntityType = Check.NotNullOrEmpty(entityType, nameof(entityType));
        WithData(nameof(EntityType), EntityType);
    }

    public string EntityType { get; }
}
