using System.Collections.Generic;
using System.Threading.Tasks;
using Service.Catalog.Dtos;
using Shared.DTO;

namespace Service.Catalog.Services
{
    public interface ICourseService
    {
        Task<Responce<List<CourseDto>>> GetAllAsync();
        Task<Responce<CourseDto>> GetByIdAsync(string id);
        Task<Responce<List<CourseDto>>> GetAllByUserId(string userid);
        Task<Responce<CourseDto>> Create(CourseCreateDto courseCreateDto);
        Task<Responce<NoContent>> UpdateAsync(CourseUpdateDto courseUpdateDto);
        Task<Responce<NoContent>> Delete(string id);
    }
}