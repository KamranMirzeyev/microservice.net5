using System.Collections.Generic;
using System.Linq;

namespace Basket.Dtos
{
    public class BasketDto
    {
        public string UserId { get; set; }
        public string Discountcode { get; set; }
        public List<BasketItemDto> BasketItems { get; set; }
        public decimal TotalPrice
        {
            get => BasketItems.Sum(x => x.Price * x.Quantity);
        }
    }
}
