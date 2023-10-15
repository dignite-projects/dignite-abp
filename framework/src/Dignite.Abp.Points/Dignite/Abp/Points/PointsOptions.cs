using Volo.Abp.Collections;

namespace Dignite.Abp.Points;

public class PointsOptions
{
    public ITypeList<IPointsDefinitionProvider> DefinitionProviders { get; private set; }

    public PointsOptions()
    {
        DefinitionProviders = new TypeList<IPointsDefinitionProvider>();
    }
}