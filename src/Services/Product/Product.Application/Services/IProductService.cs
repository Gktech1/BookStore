

using Domain.DTOs;
using Domain.Entities;
using Domain.DTOs;

namespace Application.Services
{
    public interface IProductService
    {
        Task<GenericResponse<IEnumerable<ProductResponse>>> GetProducts();
        Task<GenericResponse<ProductResponse>> GetProduct(string id);
        Task<GenericResponse<CreateProductResponse>> CreateProduct(CreateProductDto createProduct);
        /*Task<IEnumerable<Product>> GetProductByName(string name);
        Task<IEnumerable<Product>> GetProductByCategory(string categoryName);
        Task CreateProduct(Product product);
        Task<bool> UpdateProduct(Product product);
        Task<bool> DeleteProduct(long id);*/
    }
}