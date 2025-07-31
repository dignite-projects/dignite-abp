using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;

namespace Dignite.Publisher.Admin.Posts.Serialization;
public static class PostInputConverterConfigurator
{
    public static void ConfigureCreateOrUpdatePostInputConverters(this JsonSerializerOptions options, IServiceProvider serviceProvider)
    {
        var deserializers = serviceProvider.GetRequiredService<IEnumerable<IPostInputDeserializer>>();

        if (deserializers.Any())
        {
            options.Converters.Add(new CreatePostInputConverter(deserializers));
            options.Converters.Add(new UpdatePostInputConverter(deserializers));
        }
        else
        {
            throw new InvalidOperationException("No post admin deserializers registered. Ensure that you have registered at least one IPostAdminDeserializer implementation in the service collection.");
        }
    }
}
