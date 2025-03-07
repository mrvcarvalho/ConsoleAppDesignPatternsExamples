namespace StrategyNegozioOnline
{
    public interface IDiscountStrategy
    {
        public string GetDescription();
        decimal CalculateDiscount(decimal amount);
    }
}
