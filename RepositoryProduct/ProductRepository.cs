using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace RepositoryProduct
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _connectionString;

        public ProductRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Product GetById(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.QueryFirstOrDefault<Product>("SELECT * FROM Products WHERE Id = @Id", new { Id = id });
            }
        }

        public IEnumerable<Product> GetAll()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<Product>("SELECT * FROM Products");
            }
        }

        public void Add(Product product)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sql = "INSERT INTO Products (Name, Price, Stock) VALUES (@Name, @Price, @Stock)";
                db.Execute(sql, product);
            }
        }

        public void Update(Product product)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sql = "UPDATE Products SET Name = @Name, Price = @Price, Stock = @Stock WHERE Id = @Id";
                db.Execute(sql, product);
            }
        }

        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sql = "DELETE FROM Products WHERE Id = @Id";
                db.Execute(sql, new { Id = id });
            }
        }
    }




}
