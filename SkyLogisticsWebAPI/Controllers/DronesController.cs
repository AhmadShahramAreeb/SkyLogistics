using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
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

        [HttpGet("sorted/id")]
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

        [HttpGet("sorted/batteryLevel")]
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

        [HttpPost]
        public IActionResult AddDrone([FromBody] Drone drone)
        {
            try
            {
                if (drone is null)
                {
                    return BadRequest(); // 400
                }

                _manager.Drone.CreateOneDrone(drone);
                _manager.Save();

                return StatusCode(201, drone); // Created
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateDroneById([FromRoute(Name = "id")] int id,
            [FromBody] Drone drone)
        {
            try
            {
                // Drone verification , is there any Drone by this id ?
                var entity = _manager.Drone.GetOneDroneById(id, true);

                // FIRST: Check if drone exists in database
                if (entity is null)
                {
                    return NotFound($"Drone With id {id} Not Found in Database"); // 404
                }

                // SECOND: Validate that route ID matches body ID
                if (id != drone.Id)
                {
                    return BadRequest("Route ID and Body ID must match"); //400
                }

                entity.Name = drone.Name;
                entity.Model = drone.Model;
                entity.SerialNumber = drone.SerialNumber;
                entity.BatteryLevel = drone.BatteryLevel;
                entity.Status = drone.Status;

                _manager.Save();
                return Ok(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteOneDroneById([FromRoute(Name = "id")] int id)
        {
            try
            {
                // Drone verification , is there any Drone by this id ?
                var entity = _manager.Drone.GetOneDroneById(id, true);

                // FIRST: Check if drone exists in database
                if (entity is null)
                {
                    return NotFound($"Drone With id {id} Not Found in Database"); //404
                }

                _manager.Drone.DeleteOneDrone(entity);
                _manager.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPatch("{id:int}")]
        public IActionResult PartiallyUpdateDroneById([FromRoute(Name = "id")] int id,
            [FromBody] JsonPatchDocument<Drone> dronePatch)
        {
            try
            {
                // Drone verification , is there any Drone by this id ?
                var entity = _manager.Drone.GetOneDroneById(id, true);

                // FIRST: Check if drone exists in database
                if (entity is null)
                {
                    return NotFound($"Drone With id {id} Not Found in Database"); //404
                }

                dronePatch.ApplyTo(entity);
                _manager.Drone.UpdateOneDrone(entity);
                _manager.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}