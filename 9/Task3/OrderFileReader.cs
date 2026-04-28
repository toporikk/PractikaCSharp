namespace OrderAnalysisSystem
{
    public class OrderFileReader
    {
        private readonly string _filePath;

        public OrderFileReader(string filePath) => _filePath = filePath;

        public List<Order> ReadOrders()
        {
            List<Order> orders = new List<Order>();

            if (!File.Exists(_filePath)) return orders;

            foreach (string line in File.ReadAllLines(_filePath))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                string[] parts = line.Split(';');
                if (parts.Length == 3)
                {
                    int id = int.Parse(parts[0]);
                    string name = parts[1];
                    int qty = int.Parse(parts[2]);

                    orders.Add(new Order(id, name, qty));
                }
            }
            return orders;
        }
    }
}
