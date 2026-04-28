namespace OrderAnalysisSystem
{
    public class Order
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }

        public Order(int id, string name, int quantity)
        {
            Id = id;
            ProductName = name;
            Quantity = quantity;
        }
    }
}
