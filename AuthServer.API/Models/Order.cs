using System;
using System.Collections.Generic;

namespace AuthServer.API.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public decimal Total { get; set; }
        public Guid UserId { get; set; }
        public virtual ICollection<OrderBook> Books { get; set; }
    }
}