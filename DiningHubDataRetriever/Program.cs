using DiningHubDataRetriever;
using DiningHubDataRetriever.Models;

Console.WriteLine("Hello, World!");

GraphApiHandler handler = new();

List<DiningHubItem>? items = await handler.GetDiningHubItems("the-dish-at-mcalister", 10);

if (items is not null)
{
    foreach (DiningHubItem item in items)
    {
        Console.WriteLine(item);
    }
}