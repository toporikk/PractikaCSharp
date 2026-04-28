namespace FileOrderSystem
{
    public class OrderFileWriter
    {
        private readonly string _filePath;

        public OrderFileWriter(string filePath) => _filePath = filePath;

        public void WriteOrders(List<Order> orders)
        {
            using (StreamWriter writer = new StreamWriter(_filePath, false)) 
            {
                foreach (var order in orders)
                {
                    writer.WriteLine(order.ToDataString());
                }
            }
            Console.WriteLine($"Записано в файл: {orders.Count} заказов.");
        }
    }
}
