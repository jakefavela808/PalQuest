namespace AdventureS25;

public static class Items
{
    private static Dictionary<string, Item> nameToItem = 
        new Dictionary<string, Item>();
    
    public static void Initialize()
    {
        nameToItem.Clear();
        string path = Path.Combine(Environment.CurrentDirectory, "Items.json");
        string rawText = File.ReadAllText(path);
        ItemsJsonData data = System.Text.Json.JsonSerializer.Deserialize<ItemsJsonData>(rawText);
        foreach (ItemJsonData itemData in data.Items)
        {
            Item item = new Item(
                itemData.Name,
                itemData.Description,
                itemData.InitialDescription,
                itemData.IsTakeable
            );
            nameToItem.Add(item.Name, item);
            Map.AddItem(item.Name, itemData.Location);
        }
    }

    public static Item GetItemByName(string itemName)
    {
        if (nameToItem.ContainsKey(itemName))
        {
            return nameToItem[itemName];
        }
        return null;
    }
}