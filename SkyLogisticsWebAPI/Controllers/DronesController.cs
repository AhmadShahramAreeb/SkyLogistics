using Microsoft.AspNetCore.Mvc;
using Repositories.Contracts;

namespace SkyLogisticsWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    /*
     Purpose: Designed specifically for Web APIs (JSON responses)
       Contains: Essential API features only
       Model binding
       Validation
       HTTP status codes (Ok(), NotFound(), BadRequest(), etc.)
       Content negotiation
       Does NOT contain: View rendering features (no Razor, no HTML)
       Lightweight: Smaller, faster, no unnecessary features
     */
    public class DronesController : ControllerBase
    {
        private readonly IRepositoryManager _manager;

        public DronesController(IRepositoryManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public IActionResult GetAllDrones()
        {
            try
            {
                var drones = _manager.Drone.GetAllDrones(false);

                if (!drones.Any())
                {
                    return BadRequest();
                }

                return Ok(drones);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetAllDronesByBatteryLevel()
        {
            try
            {
                var drones = _manager.Drone.GetAllDronesByBatteryLevel(false);

                if (!drones.Any())
                {
                    return BadRequest();
                }

                return Ok(drones);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult GetOneDroneById([FromRoute(Name = "id")] int id)
        {
            try
            {
                var drone = _manager.Drone.GetOneDroneById(id, false);

                if (drone is null)
                {
                    return NotFound();
                }

                return Ok(drone);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}