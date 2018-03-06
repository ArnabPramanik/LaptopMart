using System.Collections.Generic;

namespace LaptopMart.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }
        public string UserId { get; set; }
        public Cart()
        {
            CartItems = new List<CartItem>();
        }

    }
}