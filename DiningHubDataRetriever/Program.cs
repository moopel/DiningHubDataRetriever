using System.Text;
using DiningHubDataRetriever;
using DiningHubDataRetriever.Models;

Console.WriteLine("Hello, World!");

GraphApiHandler handler = new();

Dictionary<string, List<DiningHubItem>> diningHubDictionary = [];


foreach (string diningHub in ConstantValues.DiningHubs)
{
    List<DiningHubItem>? items = await handler.GetDiningHubItems(diningHub, 10);
    diningHubDictionary[diningHub] = items ?? [];
}

StringBuilder sb = new();
sb.AppendLine();
foreach (var hubKvp in diningHubDictionary)
{
    sb.AppendLine($"{hubKvp.Key} items :");
    foreach (DiningHubItem item in hubKvp.Value)
    {
        sb.AppendLine($"{item}");
    }
    sb.AppendLine("");
}

Console.WriteLine(sb.ToString());