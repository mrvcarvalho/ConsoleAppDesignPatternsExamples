namespace StrategyNegozioOnline
{
    public class PercentageDiscount : DiscountStrategyBase
    {
        private readonly decimal _percentage = 0m;
        public PercentageDiscount(decimal percentage, string descriprion) : base(descriprion)
        {
            _percentage = percentage;
        }

        public override string GetDescription()
        {
            return $"{_percentage}% {Description}";
        }

        public override decimal CalculateDiscount(decimal amount)
        {
            return amount * (_percentage / 100);
        }
    }
}
