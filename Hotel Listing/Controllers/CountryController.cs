using AutoMapper;
using Hotel_Listing.Data;
using Hotel_Listing.Models;
using Hotel_Listing.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Asp.Versioning;

namespace Hotel_Listing.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    public class CountryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CountryController> _logger;
        private readonly IMapper _mapper;

        public CountryController(IUnitOfWork unitOfWork, ILogger<CountryController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;

        }
        [HttpGet]
        public async Task<IActionResult> GetCountries([FromQuery] RequestParams requestParamas)
        {
            X.PagedList.IPagedList<Country> countries = await _unitOfWork.Countries.GetPageList(requestParamas);
            IList<CountryDTO> results = _mapper.Map<IList<CountryDTO>>(countries);
            return Ok(results);


        }
        [HttpGet("{id:int}", Name = "GetCountry")]
        public async Task<IActionResult> GetCountry(int id)
        {
            Country country = await _unitOfWork.Countries.Get(q => q.Id == id, new List<string> { "Hotels" });
            CountryDTO result = _mapper.Map<CountryDTO>(country);
            return Ok(result);


        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> CreateCountry([FromBody] CreateCountryDTO countryDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
                var country = _mapper.Map<Country>(countryDTO);
                await _unitOfWork.Countries.Insert(country);
                return CreatedAtRoute("GetCountry", new { id = country.Id }, country);
            

         
        }

        [Authorize]
        [HttpPut("{Id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCountry(int Id, [FromBody] UpdateCountryDTO countryDTO)
        {
            if (!ModelState.IsValid || Id < 1)
            {
                return BadRequest(ModelState);
            }
            
                var country = await _unitOfWork.Countries.Get(q => q.Id == Id);

                if (country == null)
                {
                    _logger.LogError($"Something went wrong in the {nameof(UpdateCountry)}");
                    return BadRequest("$Submitted data is invalid");

                }
                _mapper.Map(countryDTO, country);
                _unitOfWork.Countries.Update(country);
                await _unitOfWork.Save();
                return NoContent();

        }

        [Authorize]
        [HttpDelete("{Id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCountry(int Id)
        {
            if (Id < 1)
            {
                return BadRequest(ModelState);
            }
            
                var country = await _unitOfWork.Countries.Get(q => q.Id == Id);

                if (country == null)
                {
                    _logger.LogError($"Something went wrong in the {nameof(UpdateCountry)}");
                    return BadRequest("$Submitted data is invalid");

                }
                await _unitOfWork.Countries.Delete(Id);
                await _unitOfWork.Save();
                return NoContent();

        }
    }
}
