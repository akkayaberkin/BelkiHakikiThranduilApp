using AutoMapper;
using BelkiHakiki.Core;
using BelkiHakiki.Core.DTOs;
using BelkiHakiki.Core.Repositories;
using BelkiHakiki.Core.Services;
using BelkiHakiki.Core.UnitOfWorks;

namespace BelkiHakiki.Service.Services
{
    public class CustomerService : Service<Customer>, ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomerService(IGenericRepository<Customer> repository, IUnitOfWork unitOfWork, IMapper mapper, ICustomerRepository customerRepository) : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _customerRepository = customerRepository;
        }

      
    }
}
