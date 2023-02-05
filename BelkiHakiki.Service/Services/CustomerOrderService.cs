using AutoMapper;
using BelkiHakiki.Core;
using BelkiHakiki.Core.DTOs;
using BelkiHakiki.Core.Repositories;
using BelkiHakiki.Core.Services;
using BelkiHakiki.Core.UnitOfWorks;

namespace BelkiHakiki.Service.Services
{
    public class CustomerOrderService : Service<CustomerOrder>, ICustomerOrderService
    {
        private readonly IProductService _productService;
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<CustomerOrder> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CustomerOrderService(IGenericRepository<CustomerOrder> repository, IUnitOfWork unitOfWork, IMapper mapper,  IProductService productService, ICustomerService customerService) : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _productService = productService;
            _customerService = customerService;
            _repository = repository;
            _unitOfWork=unitOfWork;
        }
  
        public async Task<CustomerOrder> SaveAsync(CustomerOrder customerOrder)
        {
            try
            {
                CustomerOrder customerOrder1 = new CustomerOrder();
                customerOrder1.Products = new List<Product>();
                customerOrder1.Customer = new Customer();
                if (!await _customerService.AnyAsync(o => o.CustomerId == customerOrder.CustomerId))
                {
                    var customer = await _customerService.AddAsync(customerOrder.Customer);
                    customerOrder.CustomerId = customer.CustomerId;
                }
                List<CustomerOrder> customerOrderProductList = new List<CustomerOrder>();
                foreach (var product in customerOrder.Products)
                {
                    var pro = await _productService.AddAsync(product);
                    var customerOrderProduct = new CustomerOrder
                    {
                        CustomerId = customerOrder.CustomerId,
                        ProductId = pro.Id
                    };
                    customerOrderProductList.Add(customerOrderProduct);
                }
                for (int i = 0; i < customerOrderProductList.Count; i++)
                {
                   await _repository.AddAsync(customerOrderProductList[i]);
                   await _unitOfWork.CommitAsync();
                }
                return customerOrder1;
                

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
