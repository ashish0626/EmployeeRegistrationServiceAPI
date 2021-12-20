using EmployeeRegistrationService.Interface;
using EmployeeRegistrationService.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeRegistrationService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class PlaceInfoController : ControllerBase
    {
        private readonly IPlaceInfoService _placeInfoService;
        public PlaceInfoController(IPlaceInfoService placeInfoService)
        {
            _placeInfoService = placeInfoService;
        }
        
        // GET api/placeinfo/id
        [HttpGet("{id}", Name = nameof(GetPlaceInfoById))]
        
        public IActionResult GetPlaceInfoById(int id)
        {
            PlaceInfo placeInfo = _placeInfoService.Find(id);
            if (placeInfo == null)
                return NotFound();
            else
                return new ObjectResult(placeInfo);
        }

        // POST api/placeinfo  
        [HttpPost]
        public IActionResult PostPlaceInfo([FromBody] PlaceInfo placeinfo)
        {
            if (placeinfo == null) return BadRequest();
            int retVal = _placeInfoService.Add(placeinfo);
            if (retVal > 0) return Ok(new { Message = "Success" }); else return NotFound();
        }
        // PUT api/placeinfo/guid  
        [HttpPut("{id}")]
        public IActionResult PutPlaceInfo(int id, [FromBody] PlaceInfo placeinfo)
        {
            if (placeinfo == null || id != placeinfo.Id) return BadRequest();
            if (_placeInfoService.Find(id) == null) return NotFound();
            int retVal = _placeInfoService.Update(placeinfo);
            if (retVal > 0) return Ok(); else return NotFound();
        }

        // DELETE api/placeinfo/5  
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            int retVal = _placeInfoService.Remove(id);
            if (retVal > 0) return Ok(); else return NotFound();
        }
    }
}
