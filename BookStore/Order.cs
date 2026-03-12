using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore
{
    public class Order
    {
        public int OrderID { get; set; }
        public string CustomerName { get; set; }
        public string ShippingAddress { get; set; }
        public List<Book> BookList { get; set; } = new List<Book>();
        public string Status { get; set; } = "Pending";
    }
}
