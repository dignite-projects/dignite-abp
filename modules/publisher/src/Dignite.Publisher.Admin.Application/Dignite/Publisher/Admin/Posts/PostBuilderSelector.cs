using System;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp.DependencyInjection;

namespace Dignite.Publisher.Admin.Posts;
public class PostBuilderSelector : IPostBuilderSelector, ITransientDependency
{
    protected IEnumerable<IPostBuilder> Builders { get; }

    public PostBuilderSelector(
        IEnumerable<IPostBuilder> postTypes)
    {
        Builders = postTypes;
    }

    public IPostBuilder Get(string postTypeName)
    {
        var builder = Builders.SingleOrDefault(t => t.PostTypeName == postTypeName);
        if (builder == null)
        {
            throw new NotSupportedException($"Post type '{postTypeName}' is not supported.");
        }

        return builder;
    }
}
