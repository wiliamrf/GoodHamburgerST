using AutoMapper;
using GoodHamburger.Business.Interfaces;
using GoodHamburger.Business.Models;
using GoodHamburger.Data.Repository;
using Microsoft.AspNetCore.Mvc;

namespace GoodHamburger.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : MainController
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        
       

        public ProductController(IProductRepository productRepository, IMapper mapper, INotifier notifier):base(notifier) 
        {
            _productRepository = productRepository;
            _mapper = mapper;
            
        }


        // Endpoint para listar sanduíches e extras
        [HttpGet]
        public async Task<IEnumerable<ProductDTO>> GetProdutos()
        {
          

            return _mapper.Map<IEnumerable<ProductDTO>>(await _productRepository.GetAll());
        }

        // Endpoint para listar apenas sanduíches
        [HttpGet("sandwiches")]
        public async Task<IEnumerable<ProductDTO>> GetSandwiches()
        {

           

            return _mapper.Map<IEnumerable<ProductDTO>>(await _productRepository.Search(P => P.productType == ProductType.Sandwich));

        }

        // Endpoint para listar apenas extras
        [HttpGet("extras")]
        public async Task<IEnumerable<ProductDTO>> GetExtras()
        {
            
            return _mapper.Map<IEnumerable<ProductDTO>>(await _productRepository.Search(P => P.productType == ProductType.Drink || P.productType == ProductType.Portion));
        }
    }
}
