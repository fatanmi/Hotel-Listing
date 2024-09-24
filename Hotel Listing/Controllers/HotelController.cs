using AutoMapper;
using Hotel_Listing.IRepository;
using Hotel_Listing.Data;
using Microsoft.AspNetCore.Mvc;
using Hotel_Listing.Models;
using Microsoft.AspNetCore.Authorization;
using Hotel_Listing.Services;

namespace Hotel_Listing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : Controller
    {
        private readonly ILogger<HotelController> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthManager _authManager;

        public HotelController(IUnitOfWork unitOfWork, ILogger<HotelController> logger, IMapper mapper, IAuthManager authManager)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _authManager = authManager;

        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetHotels()
        {
            try
            {
                IList<Hotel> Hotels = await _unitOfWork.Hotels.GetAll();
                var result = _mapper.Map<IList<HotelDTO>>(Hotels);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetHotels)}");
                return StatusCode(500, "Internal server error. Please try again later");
            }
        }

        [Authorize]
        [HttpGet("{id:int}", Name = "GetHotel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetHotel(int Id)
        {
            try
            {
                Hotel hotel = await _unitOfWork.Hotels.Get(q => q.Id == Id);
                var result = _mapper.Map<HotelDTO>(hotel);
                return Ok(result);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetHotel)}");
                return StatusCode(500, "Internal server error. Please try again later");
            }
        }
        [Authorize(Roles = "User")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreatHotel([FromBody] CreateHotelDTO hotelDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var hotel = _mapper.Map<Hotel>(hotelDTO);
                await _unitOfWork.Hotels.Insert(hotel);
                //await _unitOfWork.Save();
                return CreatedAtRoute(nameof(GetHotel), new { id = hotel.Id }, hotel);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(CreatHotel)}");
                return StatusCode(500, "Internal server error. Please try again later");
            }
        }

        [Authorize]
        [HttpPut("{Id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateHotel(int Id, [FromBody] CreateHotelDTO hotelDTO)
        {
            if (!ModelState.IsValid || Id < 1)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var hotel = await _unitOfWork.Hotels.Get(q => q.Id == Id);

                if (hotel == null)
                {
                    _logger.LogError($"Something went wrong in the {nameof(UpdateHotel)}");
                    return BadRequest("$Submitted data is invalid");

                }
                _mapper.Map(hotelDTO, hotel);
                _unitOfWork.Hotels.Update(hotel);
                await _unitOfWork.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(CreatHotel)}");
                return StatusCode(500, "Internal server error. Please try again later");
            }
        }
        [Authorize]
        [HttpDelete("{Id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteHotel(int Id)
        {
            if ( Id < 1)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var hotel = await _unitOfWork.Hotels.Get(q => q.Id == Id);

                if (hotel == null)
                {
                    _logger.LogError($"Something went wrong in the {nameof(UpdateHotel)}");
                    return BadRequest("$Submitted data is invalid");

                }
                await _unitOfWork.Hotels.Delete(Id);
                await _unitOfWork.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(DeleteHotel)}");
                return StatusCode(500, "Internal server error. Please try again later");
            }
        }
    }
}
