

using Application.Services;
using AutoMapper;
using Domain.DTOs;
using Domain.Entities;
using Domain.Repositories;
using System.Net;

namespace Domain.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<GenericResponse<IEnumerable<ProductResponse>>> GetProducts()
        {
            try
            {
                var books = await _productRepository.GetProducts();
                if (books == null)
                {
                    return new GenericResponse<IEnumerable<ProductResponse>>
                    {
                        ResponseMessage = "Book can not be found",
                        StatusCode = HttpStatusCode.NotFound,
                        Data = null,
                    };
                }

                var listOfBooks = _mapper.Map<IEnumerable<ProductResponse>>(books);

               /* foreach (var book in listOfBooks)
                {
                    var products = new Product
                    {
                        Author = book.Author,


                    };
                }*/
                return new GenericResponse<IEnumerable<ProductResponse>>
                {
                    ResponseMessage = "List of books successfully gotten",
                    StatusCode = HttpStatusCode.OK,
                    Data = listOfBooks
                };

            }
            catch (Exception ex)
            {
                return new GenericResponse<IEnumerable<ProductResponse>>
                {
                    ResponseMessage = $"an error while trying to process the request{ex.Message}",
                    StatusCode = HttpStatusCode.InternalServerError,
                    Data = null
                };
            }
        }
        public async Task<GenericResponse<ProductResponse>> GetProduct(string id)
        {
            try
            {
                var book = await _productRepository.GetProduct(id);
                if (book == null)
                {
                    return new GenericResponse<ProductResponse>
                    {
                        ResponseMessage = "Book was not found",
                        StatusCode = HttpStatusCode.NotFound,
                        Data = null,
                    };
                }
                var bookAvaliable = _mapper.Map<ProductResponse>(book);
                return new GenericResponse<ProductResponse>
                {
                    ResponseMessage = "Book successfully gotten",
                    StatusCode = HttpStatusCode.OK,
                    Data = bookAvaliable
                };

            }
            catch (Exception ex)
            {
                return new GenericResponse<ProductResponse>
                {
                    ResponseMessage = $"an error while trying to process the request{ex.Message}",
                    StatusCode = HttpStatusCode.InternalServerError,
                    Data = null
                };
            }
        }

        public async Task<GenericResponse<CreateProductResponse>> CreateProduct(CreateProductDto createProduct)
        {
            try
            {
                if (createProduct == null)
                {
                    return new GenericResponse<CreateProductResponse>
                    {
                        ResponseMessage = "Request can not be empty",
                        StatusCode = HttpStatusCode.NotFound,
                        Data = null,
                    };
                }
                var product = _mapper.Map<Product>(createProduct);
                product.ProductId = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 24);
                await _productRepository.CreateProduct(product);
                var response = new CreateProductResponse() { Id = product.ProductId };
                return new GenericResponse<CreateProductResponse>
                {
                    ResponseMessage = "Product Created Successfully",
                    StatusCode = HttpStatusCode.OK,
                    Data = response
                };
            }
            catch(Exception ex) 
            {
                return new GenericResponse<CreateProductResponse>
                {
                    ResponseMessage = $"an error while trying to process the request{ex.Message}",
                    StatusCode = HttpStatusCode.InternalServerError,
                    Data = null
                };
            }
            
        }


        /* public async Task<IEnumerable<ProductResponse>> GetProductByName(string name)
         {

             return await _productRepository.GetProductByName(name);
         }

         public async Task<IEnumerable<ProductResponse>> GetProductByCategory(string categoryName)
         {
             return await _productRepository.GetProductByCategory(categoryName);
         }

         public async Task CreateProduct(Product product)
         {
             await _productRepository.CreateProduct(product);
         }

         public async Task<bool> UpdateProduct(Product product)
         {
             return await _productRepository.UpdateProduct(product);
         }

         public async Task<bool> DeleteProduct(long id)
         {
             return await _productRepository.DeleteProduct(id);
         }*/
    }
}

