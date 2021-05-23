using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Service.Catalog.Dtos;
using Service.Catalog.Services;
using Shared.ControllerBases;

namespace Service.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : CustomControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var responce = await _courseService.GetAllAsync();

            return CreateActionResultInttance(responce);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var responce = await _courseService.GetByIdAsync(id);

            return CreateActionResultInttance(responce);
        }
        
        [Route("[action]/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetByUserId(string id)
        {
            var responce = await _courseService.GetAllByUserId(id);

            return CreateActionResultInttance(responce);
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(CourseCreateDto courseCreateDto)
        {
            var responce = await _courseService.Create(courseCreateDto);

            return CreateActionResultInttance(responce);
        }
        
        [HttpPut]
        public async Task<IActionResult> Create(CourseUpdateDto courseCreateDto)
        {
            var responce = await _courseService.UpdateAsync(courseCreateDto);

            return CreateActionResultInttance(responce);
        }
        
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var responce = await _courseService.Delete(id);

            return CreateActionResultInttance(responce);
        }
    }
}