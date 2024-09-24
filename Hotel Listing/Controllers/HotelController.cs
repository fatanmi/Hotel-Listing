using AutoMapper;
using Hotel_Listing.IRepository;
using Hotel_Listing.Data;
using Microsoft.AspNetCore.Mvc;
using Hotel_Listing.Models;
using Microsoft.AspNetCore.Authorization;
using Hotel_Listing.Services;
using Asp.Versioning;

namespace Hotel_Listing.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    public class HotelController : ControllerBase
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
        public async Task<IActionResult> GetHotels([FromQuery] RequestParams requestParams)
        {

            X.PagedList.IPagedList<Hotel> Hotels = await _unitOfWork.Hotels.GetPageList(requestParams);
            var result = _mapper.Map<IList<HotelDTO>>(Hotels);

            return Ok(result);


        }

        [Authorize]
        [HttpGet("{id:int}", Name = "GetHotel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetHotel(int Id)
        {
            Hotel hotel = await _unitOfWork.Hotels.Get(q => q.Id == Id);
            var result = _mapper.Map<HotelDTO>(hotel);
            return Ok(result);


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

            var hotel = _mapper.Map<Hotel>(hotelDTO);
            await _unitOfWork.Hotels.Insert(hotel);
            //await _unitOfWork.Save();
            return CreatedAtRoute(nameof(GetHotel), new { id = hotel.Id }, hotel);


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
        [Authorize]
        [HttpDelete("{Id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteHotel(int Id)
        {
            if (Id < 1)
            {
                return BadRequest(ModelState);
            }
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
    }
}
