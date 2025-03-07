namespace StrategyNegozioOnline
{
    public class CartItem(int id, string description, decimal price)
    {
        public int Id { get; set; } = id;
        public string Description { get; set; } = description;
        public decimal Price { get; set; } = price;
    }
}