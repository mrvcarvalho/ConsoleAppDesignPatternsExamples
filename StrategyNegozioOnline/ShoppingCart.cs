namespace StrategyNegozioOnline
{
    public class ShoppingCart
    {
        private IDiscountStrategy _discountStrategy;
        private readonly List<CartItem> _items;

        public ShoppingCart(IDiscountStrategy discountStrategy)
        {
            _discountStrategy = discountStrategy;
            _items = new List<CartItem>();
        }

        public void AddItem(CartItem item)
        {
            _items.Add(item);
        }

        public decimal CalculateTotal()
        {
            return _items.Sum(item => item.Price);
        }

        public void SetDiscountStrategy(IDiscountStrategy newDiscountStrategy)
        {
            _discountStrategy = newDiscountStrategy;
        }

        public decimal CalculateDiscountedTotal()
        {
            decimal total = CalculateTotal();
            decimal discount = _discountStrategy.CalculateDiscount(total);
            return total - discount;
        }

        public void Checkout()
        {
            Console.WriteLine("Riepilogo del carrello:");
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("ID  Descrizione                        Prezzo");
            Console.WriteLine("---------------------------------------------");


            foreach (var item in _items)
            {
                Console.WriteLine($"{item.Id,-3} {item.Description,-30} {item.Price,10:C}");
            }

            decimal totalBeforeDiscount = CalculateTotal();
            decimal totalAfterDiscount = CalculateDiscountedTotal();
            decimal discountAmount = totalBeforeDiscount - totalAfterDiscount;

            Console.WriteLine("---------------------------------------------");
            Console.WriteLine($"{"Totale prodotti:",-34} {totalBeforeDiscount,10:C}");

            string strDiscount = _discountStrategy.GetDescription();
            if (!String.IsNullOrEmpty(strDiscount))
            {
                Console.WriteLine($"{$"[{strDiscount}]",-34} {-discountAmount,10:C}");
            } 
            Console.WriteLine($"{"Totale da pagare:",-34} {totalAfterDiscount,10:C}");
        }
    }
}
