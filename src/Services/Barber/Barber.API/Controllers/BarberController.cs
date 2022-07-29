using Barber.API.Entity;
using Barber.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace Barber.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BarberController : ControllerBase
    {
        private readonly IBarberRepository _barberRepository;

        public BarberController(IBarberRepository barberRepository)
        {
            _barberRepository = barberRepository;
        }
        [HttpGet("{Id}",Name ="GetBarber")]
        [ProducesResponseType(typeof(HairDresser),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<HairDresser>> GetBarber(string Id)
        {
            var barber = await _barberRepository.GetBarber(Id);
            return Ok(barber);
        }
        [HttpPost]
        [ProducesResponseType(typeof(HairDresser), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<HairDresser>> CreateBarber([FromBody] HairDresser barber)
        {
            await _barberRepository.CreateBarber(barber);
            return CreatedAtRoute("GetBarber", new { Name = barber.Name, Family = barber.Family }, barber);
        }
        [HttpPut]
        [ProducesResponseType(typeof(HairDresser), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<HairDresser>> UpdateBarber([FromBody] HairDresser barber)
        {
            return Ok(await _barberRepository.UpdateBarber(barber));
        }
        [HttpDelete("{Id}", Name = "DeleteBarber")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<HairDresser>> DeleteBarber(string Id)
        {
            return Ok(await _barberRepository.DeleteBarber(Id));
        }

    }
}
