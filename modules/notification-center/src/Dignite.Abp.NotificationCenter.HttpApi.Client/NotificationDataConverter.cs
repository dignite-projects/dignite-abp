using Dignite.Abp.Notifications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Dignite.Abp.NotificationCenter;

public class NotificationDataConverter : JsonConverter<NotificationData>
{
    public override NotificationData Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
        {
            JsonElement root = doc.RootElement;

            if (root.TryGetProperty("type", out JsonElement typeElement))
            {
                string fullTypeName = typeElement.GetString();

                switch (fullTypeName)
                {
                    case "Dignite.Abp.Notifications.MessageNotificationData":
                        return JsonSerializer.Deserialize<MessageNotificationData>(root.GetRawText(), options);
                    case "Dignite.Abp.Notifications.LocalizableMessageNotificationData":
                        return JsonSerializer.Deserialize<LocalizableMessageNotificationData>(root.GetRawText(), options);
                    // TODO Other types of notifications should use a serialized derived class approach.
                    default:
                        throw new JsonException($"Unknown notification data type: {fullTypeName}");
                }
            }

            throw new JsonException("Type property not found in notification data");
        }
    }

    public override void Write(Utf8JsonWriter writer, NotificationData value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, value.GetType(), options);
    }
}

