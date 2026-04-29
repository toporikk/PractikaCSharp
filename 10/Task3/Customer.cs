using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public class Customer : ICustomer
    {
        private string _name;

        public Customer(string name)
        {
            _name = name;
        }

        public void Update(string discountMessage)
        {
            Console.WriteLine($"Уведомление для {_name}: Получена информация о скидке: {discountMessage}");
        }
    }
}
