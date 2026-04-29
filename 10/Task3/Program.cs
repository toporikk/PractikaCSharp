using Task3;

class Program
{
    static void Main()
    {
        OnlineStore store = new OnlineStore();

        Customer customer1 = new Customer("Алексей");
        Customer customer2 = new Customer("Мария");

        store.Subscribe(customer1);
        store.Subscribe(customer2);

        store.AnnounceDiscount("Скидка 50% на все кроссовки");

        Console.WriteLine();

        store.Unsubscribe(customer1);

        store.AnnounceDiscount("Распродажа зимней коллекции");
    }
}