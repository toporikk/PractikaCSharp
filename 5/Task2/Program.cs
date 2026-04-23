using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Supplier globalSupplier = new Supplier("Global Trade");
        Supplier localSupplier = new Supplier("Местный Фермер");

        Store[] shops = new Store[]
        {
            new Store("Продукты 24", new Supplier[] { globalSupplier, localSupplier }),
            new Store("Бутик Стиль", new Supplier[] { globalSupplier })
        };

        Customer client = new Customer("Иван");

        foreach (var shop in shops)
        {
            shop.SellProduct(client);
        }
    }
}
