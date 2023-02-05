using AutoMapper;
using BelkiHakiki.Core;
using BelkiHakiki.Core.DTOs;
using BelkiHakiki.Core.Repositories;
using BelkiHakiki.Core.Services;
using BelkiHakiki.Core.UnitOfWorks;

namespace BelkiHakiki.Service.Services
{
    public class ProductService : Service<Product>, IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IGenericRepository<Product> repository, IUnitOfWork unitOfWork, IMapper mapper, IProductRepository productRepository) : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

       
    }
}
