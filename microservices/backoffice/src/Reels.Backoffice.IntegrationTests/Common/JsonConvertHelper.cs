using System.Text.Json;

namespace Reels.Backoffice.IntegrationTests.Common;

public static class JsonConvertHelper
{
    public static T? DeserializeObject<T>(string dataString)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        return JsonSerializer.Deserialize<T>(dataString, options);
    }
}