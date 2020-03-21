using System;
using ActiveCruzer.AutoMapper;
using ActiveCruzer.BLL;
using ActiveCruzer.Models.DTO.Geo;
using ActiveCruzer.Models.Geo;
using AutoMapper;
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
        private readonly IGeoCodeBll _bll = new MemoryGeoBll();
        private IMapper _mapper;
        public GeoController(IMapper mapper) => _mapper = mapper;

        /// <summary>
        /// Try to convert an address to geo coordinates
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GeoCode")]
        public ActionResult<GetGeoCodeResponse> GetGeoCode([FromQuery] GetGeoCodeQueryParameters queryParameters )
        {
            var coordinates =  _bll.ConvertToCoordinates(_mapper.Map<GeoQuery>(queryParameters));
            return Ok(new GetGeoCodeResponse
            {
                Coordinates = _mapper.Map<CoordinatesDto>(coordinates),
                Result = GeoCodeMatchResult.Match
            });
        }
    }
}