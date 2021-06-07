using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotoStock.Dto;
using Shared.DTO;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Shared.ControllerBases;

namespace PhotoStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : CustomControllerBase
    {
        public async Task<IActionResult> PhotoSave(IFormFile file, CancellationToken cancellationToken)
        {
            if (file!=null && file.Length>0)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", file.FileName);


                await using var stream = new FileStream(path,FileMode.Create);
                await file.CopyToAsync(stream, cancellationToken);

                var returnpath = "photos/" + file.FileName;

                PhotoDto dto = new() {Url = returnpath};
                return CreateActionResultInttance(Responce<PhotoDto>.Success(dto,200));
            }

            return CreateActionResultInttance(Responce<PhotoDto>.Fail("empty", 400));
        }

        public IActionResult DeletePhoto(string phoyoUrl)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/", phoyoUrl);

            if (!System.IO.File.Exists(path))
            {
                return CreateActionResultInttance(Responce<NoContent>.Fail("path not found", 404));

            }
            System.IO.File.Delete(path);
            return CreateActionResultInttance(Responce<NoContent>.Success(200));
        }
    }
}
