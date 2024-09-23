using AutoMapper;
using Hotel_Listing.Data;
using Hotel_Listing.Models;
using Hotel_Listing.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Listing.Controllers
{
    [Route("api/[controller]")]
    public class CountryController : Controller
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
        public async Task<IActionResult> GetCountries()
        {

            try
            {
                IList<Country> countries = await _unitOfWork.Countries.GetAll();
                IList<CountryDTO> results = _mapper.Map<IList<CountryDTO>>(countries);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetCountries)}");
                return StatusCode(500, "Internal server error. Please try again later");
            }
            
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCountry(int id)
        {


            try
            {
                Country country = await _unitOfWork.Countries.Get(q => q.Id == id, new List<string> { "Hotels" });
                CountryDTO result = _mapper.Map<CountryDTO>(country);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetCountry)}");
                return StatusCode(500, "Internal server error. Please try again later");
            }
            
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {


            try
            {
                await _unitOfWork.Countries.Delete(id);
                return Ok( await GetCountries());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetCountry)}");
                return StatusCode(500, "Internal server error. Please try again later");
            }
            
        }
    }
}
