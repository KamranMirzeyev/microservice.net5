using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MassTransit;
using MongoDB.Driver;
using Service.Catalog.Dtos;
using Service.Catalog.Models;
using Service.Catalog.Settings;
using Shared.DTO;
using Shared.Messages;

namespace Service.Catalog.Services
{
    public class CourseService : ICourseService
    {
        private readonly IMongoCollection<Course> _courseCollection;
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public CourseService(IMapper mapper, IDatabaseSetting databaseSetting, IPublishEndpoint publishEndpoint)
        {
            var client = new MongoClient(databaseSetting.ConnectionString);
            var database = client.GetDatabase(databaseSetting.DatabaseName);
            _courseCollection = database.GetCollection<Course>(databaseSetting.CourseCollectionName);
            _categoryCollection = database.GetCollection<Category>(databaseSetting.CategoryCollectionName);
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<Responce<List<CourseDto>>> GetAllAsync()
        {
            var courses = await _courseCollection.Find<Course>(cours => true).ToListAsync();
            if (courses.Any())
            {
                foreach (var course in courses)
                {
                    course.Catagory = await _categoryCollection.Find(x => x.Id == course.CatagoryId).FirstAsync();
                }
            }
            else
            {
                courses = new List<Course>();
            }

            return Responce<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), 200);
        }

        public async Task<Responce<CourseDto>>GetByIdAsync(string id)
        {
            var course = await _courseCollection.Find<Course>(x => x.Id == id).FirstOrDefaultAsync();

            if (course==null)
            {
                return Responce<CourseDto>.Fail("course tapılmadı", 404);
            }

            course.Catagory = await _categoryCollection.Find(x => x.Id == course.CatagoryId).FirstAsync();
            return Responce<CourseDto>.Success(_mapper.Map<CourseDto>(course), 200);
        }

        public async Task<Responce<List<CourseDto>>>GetAllByUserId(string userid)
        {
            var courses = await _courseCollection.Find<Course>(x => x.UserId == userid).ToListAsync();
            if (courses.Any())
            {
                foreach (var course in courses)
                {
                    course.Catagory = await _categoryCollection.Find(x => x.Id == course.CatagoryId).FirstAsync();
                }
            }
            else
            {
                courses = new List<Course>();
            }

            return Responce<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), 200);
        }

        public async Task<Responce<CourseDto>> Create(CourseCreateDto courseCreateDto)
        {
            var newcourse = _mapper.Map<CourseCreateDto, Course>(courseCreateDto);

            newcourse.CreateTime = DateTime.Now;;
            await _courseCollection.InsertOneAsync(newcourse);

            return Responce<CourseDto>.Success(_mapper.Map<CourseDto>(newcourse), 201);
        }

        public async Task<Responce<NoContent>> UpdateAsync(CourseUpdateDto courseUpdateDto)
        {
            var updatecourse = _mapper.Map<Course>(courseUpdateDto);
            var result = await _courseCollection.FindOneAndReplaceAsync(x => x.Id == courseUpdateDto.Id, updatecourse);
            if (result==null)
            {
                return Responce<NoContent>.Fail("Not found", 404);
            }

            await _publishEndpoint.Publish<CourseNameChangedEvent>(new CourseNameChangedEvent { CourseId = updatecourse.Id, UpdateName = courseUpdateDto.Name });
            return Responce<NoContent>.Success(204);
        }

        public async Task<Responce<NoContent>> Delete(string id)
        {
            var result = await _courseCollection.DeleteOneAsync(x => x.Id == id);

            if (result.DeletedCount>0)
            {
                return Responce<NoContent>.Success(204);
            }

            return Responce<NoContent>.Fail("not found", 404);
        }
 
    }
}