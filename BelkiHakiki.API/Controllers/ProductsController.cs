//using AutoMapper;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using BelkiHakiki.API.Filters;
//using BelkiHakiki.Core;
//using BelkiHakiki.Core.DTOs;
//using BelkiHakiki.Core.Services;

//namespace BelkiHakiki.API.Controllers
//{

//    public class ProductsController : CustomBaseController
//    {
//        private readonly IMapper _mapper;
//        private readonly IProductService _service;

//        public ProductsController(IMapper mapper, IProductService productService)
//        {

//            _mapper = mapper;
//            _service = productService;
//        }

//        /// GET api/products
//        [HttpGet]
//        public async Task<IActionResult> All()
//        {
//            var products = await _service.GetAllAsync();
//            var productsDtos = _mapper.Map<List<ProductDto>>(products.ToList());
//            return CreateActionResult(CustomResponseDto<List<ProductDto>>.Success(200, productsDtos));
//        }


//        [ServiceFilter(typeof(NotFoundFilter<Product>))]
//        // GET /api/products/5
//        [HttpGet("{id}")]
//        public async Task<IActionResult> GetById(int id)
//        {
//            var product = await _service.GetByIdAsync(id);
//            var productsDto = _mapper.Map<ProductDto>(product);
//            return CreateActionResult(CustomResponseDto<ProductDto>.Success(200, productsDto));
//        }



//        [HttpPost("[action]")]
//        public async Task<IActionResult> Save(ProductSaveDto productDto)
//        {
//            var product = await _service.AddAsync(_mapper.Map<Product>(productDto));
//            var productsDto = _mapper.Map<ProductSaveDto>(product);
//            return CreateActionResult(CustomResponseDto<ProductSaveDto>.Success(201, productsDto));
//        }


//        [HttpPut]
//        public async Task<IActionResult> Update(ProductUpdateDto productDto)
//        {
//            var product = _service.Where(o => o.Id == productDto.Id).FirstOrDefault();
//            if (product == null)
//                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(404, "Ürün bulunamadı."));
//            product.Quantity = productDto.Quantity;
//            product.Id = productDto.Id;
//            await _service.UpdateAsync(product);
//            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
//        }

//        // DELETE api/products/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> Remove(int id)
//        {
//            var product = await _service.GetByIdAsync(id);
//            await _service.RemoveAsync(product);

//            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
//        }

//    }
//}
///*
//* Bu kod, ürün yönetimi işlemleri için RESTful API'lerin uygulandığı bir Ürünler Kontrolcüsü sınıfıdır.
//* Kategorileri olan ürünleri almak, tüm ürünleri almak, bir id'ye göre ürün almak, ürün kaydetmek, ürün güncellemek ve ürün kaldırmak gibi işlemleri yapmak için kullanılır.
//* Kontrolcü sınıfı, IMapper arayüzünü kaynak verileri istenen formata dönüştürmek için ve IProductService arayüzünü arka uç hizmetiyle iletişim kurmak için kullanır.
//*/