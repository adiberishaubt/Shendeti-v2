using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Nodes;
using Shendeti.Infrastructure.Interfaces;

namespace Shendeti.Infrastructure.Services;

public class MeetingService : IMeetingService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public MeetingService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    private async Task<string> GetAccessToken()
    {
        var url = "https://zoom.us/oauth/token?grant_type=account_credentials&account_id=afo60fuITS-LxFRBp4QhHQ";
        var authorizationHeader = "Basic dW5LX1c0YnZRbjJkS0twTFpBNUFEdzpBdWlLVDBnbktrNTVRNVk4cjR1RkVGSGhDOHo0REFRcg==";
        
        using var httpClient = _httpClientFactory.CreateClient();
        httpClient.DefaultRequestHeaders.Add("Authorization", authorizationHeader);

        var response = await httpClient.PostAsync(url, null);
            
        var responseBody = await response.Content.ReadAsStringAsync();
        var body = JsonSerializer.Deserialize<JsonObject>(responseBody);
        return body["access_token"].GetValue<string>();
    }

    public async Task<string> CreateMeeting()
    {
        var url = "https://api.zoom.us/v2/users/me/meetings";
        var accessToken = await GetAccessToken();

        using var client = _httpClientFactory.CreateClient();
        
        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
        
        var requestBody = new
        {
            agenda = "My Meeting",
            default_password = false,
            duration = 30,
            password = "",
            pre_schedule = false,
            schedule_for = "granitbunjaku14@gmail.com",
            start_time = "2022-03-25T07:32:55Z",
            timezone = "Europe/Budapest",
            topic = "My Meeting",
            type = 2,
            settings = new
            {
                join_before_host = true
            }
        };

        var response = await client.PostAsJsonAsync(url, requestBody);
        var responseBody = await response.Content.ReadAsStringAsync();
        
        var body = JsonSerializer.Deserialize<JsonObject>(responseBody);
        return body["join_url"].GetValue<string>();
    }
}