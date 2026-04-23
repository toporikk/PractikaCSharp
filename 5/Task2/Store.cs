class Store
{
    public string StoreName { get; set; }

    private Supplier[] _suppliers;

    private Inventory _inventory = new Inventory();

    public Store(string name, Supplier[] suppliers)
    {
        StoreName = name;
        _suppliers = suppliers;
    }

    public void SellProduct(Customer customer)
    {
        if (_inventory.ItemsCount > 0)
        {
            _inventory.ItemsCount--;
            Console.WriteLine($"Магазин '{StoreName}' продал товар покупателю {customer.Name}. Остаток: {_inventory.ItemsCount}");
        }
        else
        {
            Console.WriteLine($"В магазине '{StoreName}' закончился товар!");
        }
    }
}
