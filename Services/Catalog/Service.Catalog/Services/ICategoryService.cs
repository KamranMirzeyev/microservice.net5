using System.Collections.Generic;
using System.Threading.Tasks;
using Service.Catalog.Dtos;
using Service.Catalog.Models;
using Shared.DTO;

namespace Service.Catalog.Services
{
    public interface ICategoryService
    {
        Task<Responce<CategoryDto>> GetByIdAsync(string id);
        Task<Responce<CategoryDto>> CreateAsync(CategoryDto categoryDto);
        Task<Responce<List<CategoryDto>>> GetAllAsync();
    }
}