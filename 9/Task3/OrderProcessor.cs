namespace OrderAnalysisSystem
{
    public class OrderProcessor
    {
        private List<Order> _orders;

        public OrderProcessor(List<Order> orders)
        {
            _orders = orders;
        }

        public void GetMostPopularProducts(int topN)
        {
            Console.WriteLine($"--- Топ-{topN} популярных товаров ---");

            var popular = _orders
                .GroupBy(o => o.ProductName)
                .Select(group => new
                {
                    Name = group.Key,
                    TotalQuantity = group.Sum(o => o.Quantity)
                })
                .OrderByDescending(res => res.TotalQuantity)
                .Take(topN);

            foreach (var item in popular)
            {
                Console.WriteLine($"{item.Name}: куплено {item.TotalQuantity} шт.");
            }
        }
    }
}
