using System;
using System.Collections.Generic;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;

namespace Dignite.Publisher.Admin.Posts.Serialization;
public static class PostAdminDtoConverterConfigurator
{
    public static void ConfigurePostAdminDtoConverters(this JsonSerializerOptions options, IServiceProvider serviceProvider)
    {
        var deserializers = serviceProvider.GetRequiredService<IEnumerable<IPostAdminDtoDeserializer>>();
        options.Converters.Add(new PostAdminDtoConverter(deserializers));
    } 
}
