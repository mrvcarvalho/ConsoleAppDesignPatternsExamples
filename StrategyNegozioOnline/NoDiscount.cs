namespace StrategyNegozioOnline
{
    public class NoDiscount : DiscountStrategyBase
    {
        public NoDiscount() : base(string.Empty)
        {
        }
        public override string GetDescription()
        {
            return Description;
        }

        public override decimal CalculateDiscount(decimal amount)
        {
            return 0;
        }
    }
}

