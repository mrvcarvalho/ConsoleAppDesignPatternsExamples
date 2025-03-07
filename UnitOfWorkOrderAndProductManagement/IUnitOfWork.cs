namespace UnitOfWorkOrderAndProductManagement
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Product> Products { get; }
        IRepository<Order> Orders { get; }
        int Complete();
    }
}
