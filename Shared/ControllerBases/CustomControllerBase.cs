using Microsoft.AspNetCore.Mvc;
using Shared.DTO;

namespace Shared.ControllerBases
{
    public class CustomControllerBase : ControllerBase
    {
        public IActionResult CreateActionResultInttance<T>(Responce<T> responce)
        {
            return new ObjectResult(responce)
            {
                StatusCode = responce.StatusCode
            };
        }
    }
}