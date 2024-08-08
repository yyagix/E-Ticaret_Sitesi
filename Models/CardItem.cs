namespace ProductApp.Models
{
    public class CardItem
    {
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public decimal Total
        {
            get
            {
                return Quantity * Price;
            }
        }
        public CardItem() { }

        public CardItem(Products products)
        {
            ProductId = products.ProductId;
            ProductName = products.ProductName;
            Quantity = 1;
            Price = Convert.ToDecimal(products.ProductPrice);
            Image = products.ProductPicture;
        }
    }
}
