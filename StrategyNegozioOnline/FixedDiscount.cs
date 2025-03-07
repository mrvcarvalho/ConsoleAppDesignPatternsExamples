namespace StrategyNegozioOnline
{
    public class FixedDiscount : DiscountStrategyBase
    {
        private readonly decimal _discountAmount = 0m;

        public override string GetDescription()
        {
            return $"€{_discountAmount} {Description}";
        }

        public FixedDiscount(decimal discountAmount, string descriprion) : base(descriprion)
        {
            _discountAmount = discountAmount;
        }

        public override decimal CalculateDiscount(decimal amount)
        {
            return Math.Min(amount, _discountAmount);
        }
    }
}
