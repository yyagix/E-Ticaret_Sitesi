using ProductApp.Models;

namespace ProductApp.Dto
{
    public class CardViewModel
    {
        public List<CardItem> CardItems { get; set; }

        public decimal GrandTotal
        {
            get
            {
                return CardItems?.Sum(x => x.Quantity * x.Price) ?? 0;
            }
        }
    }



}
