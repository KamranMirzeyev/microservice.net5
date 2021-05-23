using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MongoDB.Driver;
using Service.Catalog.Dtos;
using Service.Catalog.Models;
using Service.Catalog.Settings;
using Shared.DTO;

namespace Service.Catalog.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;

        public CategoryService(IMapper mapper, IDatabaseSetting databaseSetting)
        {
            var client = new MongoClient(databaseSetting.ConnectionString);
            var database = client.GetDatabase(databaseSetting.DatabaseName);
            _categoryCollection = database.GetCollection<Category>(databaseSetting.CategoryCollectionName);
            _mapper = mapper;
        }

        public async Task<Responce<List<CategoryDto>>> GetAllAsync()
        {
            var catogories = await _categoryCollection.Find<Category>(category => true).ToListAsync();
            var data = _mapper.Map<List<Category>, List<CategoryDto>>(catogories);
            return Responce<List<CategoryDto>>.Success(data, 200);
        }

        public async Task<Responce<CategoryDto>> CreateAsync(CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            await _categoryCollection.InsertOneAsync(category);
            return Responce<CategoryDto>.Success(_mapper.Map<CategoryDto>(category),201);
        }

        public async Task<Responce<CategoryDto>> GetByIdAsync(string id)
        {
            var category = await _categoryCollection.Find<Category>(x => x.Id == id).FirstOrDefaultAsync();
            if (category==null)
            {
                return Responce<CategoryDto>.Fail("categoriya tapılmadı", 404);
            }

            return Responce<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), 200);
        }
    }
}