namespace FileOrderSystem
{
    public class Order
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }

        public Order(int id, string productName, int quantity)
        {
            Id = id;
            ProductName = productName;
            Quantity = quantity;
        }

        public string ToDataString() => $"{Id};{ProductName};{Quantity}";

        public override string ToString() => $"ID: {Id}, Товар: {ProductName}, Кол-во: {Quantity}";
    }
}
