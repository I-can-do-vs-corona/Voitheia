using System;
using ActiveCruzer.Models.DTO.Geo;
using Microsoft.AspNetCore.Mvc;

namespace ActiveCruzer.Controllers
{
    /// <summary>
    /// Controller to validate addresses and convert to geo coordinates
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class GeoController : BaseController
    {
        [HttpGet]
        [Route("GeoCode")]
        public ActionResult<GetGeoCodeResponse> GetGeoCode([FromQuery] GetGeoCodeQueryParameters queryParameters )
        {
            throw new NotImplementedException();
        }
    }
}