using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Service.Catalog.Dtos;
using Service.Catalog.Services;
using Shared.ControllerBases;

namespace Service.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : CustomControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var responce = await _categoryService.GetAllAsync();
            return CreateActionResultInttance(responce);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var responce = await _categoryService.GetByIdAsync(id);
            return CreateActionResultInttance(responce);
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(CategoryDto categoryDto)
        {
            var responce = await _categoryService.CreateAsync(categoryDto);
            return CreateActionResultInttance(responce);
        }
    }
}