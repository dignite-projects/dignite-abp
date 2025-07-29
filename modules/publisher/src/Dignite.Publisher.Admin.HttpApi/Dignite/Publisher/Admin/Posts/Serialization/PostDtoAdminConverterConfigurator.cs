using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;

namespace Dignite.Publisher.Admin.Posts.Serialization;
public static class PostDtoAdminConverterConfigurator
{
    public static void ConfigureCreateOrUpdatePostDtoConverters(this JsonSerializerOptions options, IServiceProvider serviceProvider)
    {
        var deserializers = serviceProvider.GetRequiredService<IEnumerable<IPostAdminDeserializer>>();

        if (deserializers.Any())
        {
            options.Converters.Add(new CreatePostDtoConverter(deserializers));
            options.Converters.Add(new UpdatePostDtoConverter(deserializers));
        }
        else
        {
            throw new InvalidOperationException("No post admin deserializers registered. Ensure that you have registered at least one IPostAdminDeserializer implementation in the service collection.");
        }
    }
}
