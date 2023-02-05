using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BelkiHakiki.API.Filters;
using BelkiHakiki.Core;
using BelkiHakiki.Core.DTOs;
using BelkiHakiki.Core.Services;

namespace BelkiHakiki.API.Controllers
{

    public class CustomerOrderController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly ICustomerOrderService _customerOrderService;
        private readonly ICustomerService _customerService;
        private readonly IProductService _productService;

        public CustomerOrderController(IMapper mapper, ICustomerOrderService customerOrderService, IProductService productService, ICustomerService customerService)
        {

            _mapper = mapper;
            _customerOrderService = customerOrderService;
            _productService = productService;
            _customerService = customerService;
        }

        /// GET api/customerOrder
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var customerOrders = await _customerOrderService.GetAllAsync();
            var customerOrderDtoList = BuildCustomerOrderData(customerOrders);
            var customerOrderDtos = _mapper.Map<List<CustomerOrderDto>>(customerOrderDtoList.ToList());
            return CreateActionResult(CustomResponseDto<List<CustomerOrderDto>>.Success(200, customerOrderDtos));
        }

        // GET /api/customerOrder/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var customerOrder = _customerOrderService.Where(o => o.Id == id).FirstOrDefault();
            var customerOrderDto = BuildCustomerOrderData(customerOrder);
            return CreateActionResult(CustomResponseDto<CustomerOrderDto>.Success(200, customerOrderDto));

        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Save(CustomerOrderDto customerOrderDto)
        {
            var customerOrder = _mapper.Map<CustomerOrder>(customerOrderDto);
            var customerOrderResp = await _customerOrderService.SaveAsync(customerOrder);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(200));
        }

        [HttpPut]
        public async Task<IActionResult> Update(CustomerOrderUpdateDto customerOrderDto)
        {
            try
            {
                var customerOrder = _customerOrderService.Where(o => o.CustomerId == customerOrderDto.Customer.Id).FirstOrDefault();
                var customerOrderDtos = BuildCustomerOrderData(customerOrder);
                await UpdateCustomerAddress(customerOrderDto.Customer.Address, customerOrderDtos.Customer.Id);
                await UpdateProduct(customerOrderDto);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        private async Task UpdateProduct(CustomerOrderUpdateDto customerOrderDto)
        {
            foreach (var item in customerOrderDto.ProductList)
            {
                var existingProduct = _productService.Where(p => p.Id == item.Id).FirstOrDefault();
                if (existingProduct != null)
                {
                    existingProduct.Quantity = item.Quantity;
                    await _productService.UpdateAsync(existingProduct);
                }
            }
        }

        // DELETE api/customerOrder/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {   //Silme işlemi için product bazında silmeler yapılarak silinmesi daha doğru olur diye düşündüm bu sebeple customerOrder ın Id sini kullandım.
            var customerOrder = _customerOrderService.Where(o => o.Id == id).FirstOrDefault();
            await _customerOrderService.RemoveAsync(customerOrder);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        #region Helper Methods
        private List<CustomerOrderDto> BuildCustomerOrderData(IEnumerable<CustomerOrder> customerOrders)
        {
            List<CustomerOrderDto> customerOrderDtoList = new List<CustomerOrderDto>();
            var newCostumerOrders = customerOrders.DistinctBy(o=>o.CustomerId);
            foreach (var customerOrder in newCostumerOrders)
            {
                var productIds = _customerOrderService.Where(o => o.CustomerId == customerOrder.CustomerId).Select(o => o.ProductId).ToList();
                var products = _productService.Where(p => productIds.Contains(p.Id)).ToList();
                var customer = _customerService.Where(o => o.CustomerId == customerOrder.CustomerId).FirstOrDefault();
                var productDto = _mapper.Map<List<ProductDto>>(products.ToList());
                customerOrder.Products = products;
                customerOrder.Customer = customer;
                customerOrderDtoList.Add(_mapper.Map<CustomerOrderDto>(customerOrder));
            }
            return customerOrderDtoList;
        }

        private CustomerOrderDto BuildCustomerOrderData(CustomerOrder customerOrder)
        {
            CustomerOrderDto _customerOrderDto = new CustomerOrderDto();
            var productIds = _customerOrderService.Where(o => o.CustomerId == customerOrder.CustomerId).Select(o => o.ProductId).ToList();
            var products = _productService.Where(p => productIds.Contains(p.Id)).ToList();
            var customer = _customerService.Where(o => o.CustomerId == customerOrder.CustomerId).FirstOrDefault();
            var productDto = _mapper.Map<List<ProductSaveDto>>(products.ToList());
            var customerDto = _mapper.Map<CustomerDto>(customer);
            _customerOrderDto.Products = productDto;
            _customerOrderDto.Customer = customerDto;
            _customerOrderDto.Customer.Id = customerOrder.CustomerId;

            return _customerOrderDto;
        }

        private async Task UpdateCustomerAddress(string customerAddress, int customerId)
        {
            Customer customer = _customerService.Where(o => o.CustomerId == customerId).FirstOrDefault();
            customer.Address = customerAddress;
            await _customerService.UpdateAsync(customer);
        }
        #endregion

    }
}
