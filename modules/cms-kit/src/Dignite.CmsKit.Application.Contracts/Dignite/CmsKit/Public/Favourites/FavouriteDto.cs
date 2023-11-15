using System;

namespace Dignite.CmsKit.Public.Favourites;

[Serializable]
public class FavouriteDto
{
    public Guid Id { get; set; }

    public string EntityType { get; set; }

    public string EntityId { get; set; }

    public Guid CreatorId { get; set; }

    public DateTime CreationTime { get; set; }
}
