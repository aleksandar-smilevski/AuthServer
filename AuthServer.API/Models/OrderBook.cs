using System;

namespace AuthServer.API.Models
{
    public class OrderBook
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public Guid BookId { get; set; }
        public Book Book { get; set; }
    }
}