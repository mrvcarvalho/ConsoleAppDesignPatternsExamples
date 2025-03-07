using System.Data;

namespace RepositoryProduct
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public void AddNewProduct(string name, decimal price, int stock)
        {
            var product = new Product
            {
                Name = name,
                Price = price,
                Stock = stock
            };

            _productRepository.Add(product);
        }

        public void UpdateProductStock(int productId, int newStock)
        {
            var product = _productRepository.GetById(productId);
            if (product != null)
            {
                product.Stock = newStock;
                _productRepository.Update(product);
            }
        }

        public IEnumerable<Product> GetLowStockProducts(int threshold)
        {
            return _productRepository.GetAll().Where(p => p.Stock < threshold);
        }
    }




}
