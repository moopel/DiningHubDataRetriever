using System.Net;
using System.Net.ServerSentEvents;
using System.Text.Json;
using DiningHubDataRetriever.Models;

namespace DiningHubDataRetriever;

public class GraphApiHandler
{
    private readonly HttpClient _client;

    private const string Endpoint = ConstantValues.DiningHubBaseEndpoint;
    private readonly JsonSerializerOptions _serializerOptions;

    public GraphApiHandler()
    {
        _client = new HttpClient();

        _client.DefaultRequestHeaders.Add("x-api-key", "ElevateAPIProd");
        _client.DefaultRequestHeaders.Add("store", "ch_clemson_en");

        _client.BaseAddress = new Uri(Endpoint);

        _serializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }

    private async Task<string> CallGetGraphApi(string addToUrl, HttpStatusCode expectedStatus)
    {
        HttpResponseMessage response = await _client.GetAsync(addToUrl);
        string responseString = await response.Content.ReadAsStringAsync();

        if (response.StatusCode != expectedStatus)
        {
            throw new Exception($"{response.StatusCode} - {responseString}");
        }

        return responseString;
    }

    public async Task<List<DiningHubItem>?> GetDiningHubItems(string location, int mealPeriod)
    {
        try
        {
            string date = DateTime.UtcNow.ToString("yyyy-MM-dd");

            Dictionary<string, string> queryParams = new()
            {
                ["query"] = ConstantValues.MenuItemQuery,
                ["operationName"] = "getLocationRecipes",
                ["variables"] = $"{{\"campusUrlKey\":\"campus\",\"locationUrlKey\":\"{location}\",\"date\":\"{date}\",\"mealPeriod\":{mealPeriod},\"viewType\":\"DAILY\"}}"
            };

            string queryParameterString = BuildQueryParameters(queryParams);

            string json = await CallGetGraphApi(queryParameterString, HttpStatusCode.OK);

            GraphMenuResponse parsed = JsonSerializer.Deserialize<GraphMenuResponse>(json, _serializerOptions)
            ?? throw new("Failed to parse json into [GraphMenuResponse]");

            List<GraphMenuItem>? items = (parsed.Data?.Recipes?.Products?.Items)
            ?? throw new("Failed to parse Items into [GraphMenuItems]");

            List<DiningHubItem> diningHubItems = [];

            foreach (GraphMenuItem item in items)
            {
                if (item is not null)
                {
                    string? ingredients = item.Attributes?.FirstOrDefault(a => a.Name == "recipe_ingredients")?.Value.ToString();

                    string? allergens = item.Attributes?.FirstOrDefault(a => a.Name == "allergen_statement")?.Value.ToString();

                    diningHubItems.Add(new()
                    {
                        Name = item.Name ?? "Failed to get Name",
                        SkuId = item.Sku ?? "Failed to get Sku",
                        Ingredients = ingredients ?? "Failed to get Ingredients",
                        Allergens = allergens ?? "Failed to get Allergens"
                    });
                }
            }
            return diningHubItems;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }

    private static string BuildQueryParameters(Dictionary<string, string> queryParams)
    {
        if (queryParams == null || queryParams.Count == 0)
            return string.Empty;

        List<string> parts = [];

        foreach (var param in queryParams)
        {
            string key = WebUtility.UrlEncode(param.Key);
            string value = WebUtility.UrlEncode(param.Value);

            parts.Add($"{key}={value}");
        }

        return "?" + string.Join("&", parts);
    }
}
