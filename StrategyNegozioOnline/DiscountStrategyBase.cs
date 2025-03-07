namespace StrategyNegozioOnline
{
    public abstract class DiscountStrategyBase : IDiscountStrategy
    {
        public string Description {get;}

        protected DiscountStrategyBase(string descriprion)
        {
            Description = descriprion;
        }

        public abstract string GetDescription();

        public abstract decimal CalculateDiscount(decimal amount);

    }
}