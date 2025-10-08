namespace ClientApp.Model;

public class Aflevering2
{
    private List<Item> items;

    public Aflevering2()
    {
        Item i1 = new Item { Name = "Item1", Price = 123, Stock = 1, Color = "red" };
        Item i2 = new Item { Name = "Item2", Price = 12, Stock = 0, Color = "green" };
        Item i3 = new Item { Name = "Item3", Price = 23, Stock = 2, Color = "black" };
        Item i4 = new Item { Name = "Item4", Price = 10, Stock = 0, Color = "blue" };
        Item i5 = new Item { Name = "Item5", Price = 12323, Stock = 12, Color = "green" };

        items = [i1, i2, i3, i4, i5];
    }

    public List<Item> Items => items;

    public List<Item> ItemsInStock()
    {
        List<Item> result = new();
        
        foreach (var item in items)
            if (item.Stock > 0)
                result.Add(item);
        return result;
    }

    public List<Item> ItemsWithColor(string[] colors)
    {
        List<Item> result = new();
        
        foreach (var item in items)
            if (colors.Contains(item.Color))
                result.Add(item);
        return result;
        
    }
}