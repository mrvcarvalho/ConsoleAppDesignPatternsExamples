namespace UnitOfWorkOrderAndProductManagement
{
    public class OrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void CreateOrder(int productId, int quantity)
        {
            var product = _unitOfWork.Products.GetById(productId);
            if (product == null)
            {
                throw new Exception("Prodotto non trovato");
            }

            if (product.Stock < quantity)
            {
                throw new Exception("Scorta insufficiente");
            }

            var order = new Order
            {
                ProductId = productId,
                Quantity = quantity,
                OrderDate = DateTime.Now
            };

            _unitOfWork.Orders.Add(order);
            product.Stock -= quantity;
            _unitOfWork.Products.Update(product);

            _unitOfWork.Complete();
        }
    }
}
