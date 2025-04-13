using Reels.Backoffice.Api.DTOs;

namespace Reels.Backoffice.IntegrationTests.Common;

public static class DeserializeResponseHelper
{
    public static async Task<CustomResponse> Response(HttpResponseMessage response)
    {
        var responseRequest = await response.Content.ReadAsStringAsync();
        return JsonConvertHelper.DeserializeObject<CustomResponse>(responseRequest);
    }
}