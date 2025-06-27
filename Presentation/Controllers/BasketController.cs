using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DatatoObject_Dtos_.BasketDtos;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketController : ControllerBase
    {
        readonly IServiceManager serviceManager;
        public BasketController(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }

        [HttpGet]
        public async Task<ActionResult<BasketDto>> GetBasket(string id)
        {
            var basket = await serviceManager.BasketService.GetBasket(id);
            return Ok(basket);
        }
        [HttpPost]
        public async Task<ActionResult<BasketDto>> CreateOrUpdateBasket(BasketDto basketDto)
        {
            var updatedBasket = await serviceManager.BasketService.CreateorUpdateBasket(basketDto);
            return Ok(updatedBasket);
        }
        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteBasket(string id)
        {
            var isDeleted = await serviceManager.BasketService.DeleteBasket(id);
            if (isDeleted)
            {
                return Ok(value: true);
            }
            return NotFound("Basket not found or could not be deleted.");
        }
    }
}
