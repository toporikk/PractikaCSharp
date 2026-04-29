using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public class OnlineStore
    {
        private readonly List<ICustomer> _customers = new List<ICustomer>();

        public void Subscribe(ICustomer customer)
        {
            _customers.Add(customer);
        }

        public void Unsubscribe(ICustomer customer)
        {
            _customers.Remove(customer);
        }

        public void AnnounceDiscount(string discountInfo)
        {
            Console.WriteLine($"Магазин: Объявляем акцию — {discountInfo}!");
            foreach (var customer in _customers)
            {
                customer.Update(discountInfo);
            }
        }
    }
}
