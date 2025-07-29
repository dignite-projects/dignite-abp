using System;
using System.Collections.Generic;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;

namespace Dignite.Publisher.Posts.Serialization;
public static class PostDtoConverterConfigurator
{
    public static void ConfigurePostDtoConverters(this JsonSerializerOptions options, IServiceProvider serviceProvider)
    {
        var deserializers = serviceProvider.GetRequiredService<IEnumerable<IPostDeserializer>>();
        options.Converters.Add(new PostDtoConverter(deserializers));
    } 
}
