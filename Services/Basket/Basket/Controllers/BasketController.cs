using Basket.Dtos;
using Basket.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.ControllerBases;
using Shared.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : CustomControllerBase
    {
        private readonly IBasketService _basketService;
        private readonly ISharedIdentityService _sharedIdentityService;

        public BasketController(IBasketService basketService, ISharedIdentityService sharedIdentityService)
        {
            _basketService = basketService;
            _sharedIdentityService = sharedIdentityService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return CreateActionResultInttance(await _basketService.GetBasket(_sharedIdentityService.GetUserId));
        }

        [HttpPost]
        public async Task<IActionResult> SaveOrUpdate(BasketDto basketDto)
        {
            var reposnce = await _basketService.SaveOrUpdate(basketDto);
            return CreateActionResultInttance(reposnce);
        }

        public async Task<IActionResult> Delete()
        {
            return CreateActionResultInttance(await _basketService.Delete(_sharedIdentityService.GetUserId));
        }


    }
}
