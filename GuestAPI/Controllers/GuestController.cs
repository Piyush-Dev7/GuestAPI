using DataAccess.Core.Repositories.Contract;
using GuestsAPI.Auth;
using GuestSharedModel.DtoModels;
using Microsoft.AspNetCore.Mvc;
using GuestSharedModel.Enums;
using DataAccess.Core.Helpers;

namespace GuestAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApplicationAuthorize]
    public class GuestController : ControllerBase
    {
        private readonly ILogger<GuestController> _logger;
        private readonly IGuestRepository _guestRepository;

        public GuestController(ILogger<GuestController> logger, IGuestRepository guestRepository)
        {
            _logger = logger;
            _guestRepository = guestRepository;
        }

        /// <summary>
        /// Creates a new guest entity record in the system.
        /// </summary>
        /// <param name="guest">New Guest information</param>
        /// <returns>Created status with guest id of the created guest, 
        /// on failure returns the bad request status with appropriate message.</returns>
        [HttpPost]
        public async Task<IActionResult> AddGuest(GuestDto guest)
        {
            try
            {
                var status = await _guestRepository.Create(guest, GetUserId());
                switch (status)
                {
                    case GuestStatus.PhoneRequired:
                        return BadRequest("At least one phone number is required.");
                    case GuestStatus.NameRequired:
                        return BadRequest("At least one name is required.");
                    case GuestStatus.PhoneInvalid:
                        return BadRequest("Not a valid phone number.");
                    case GuestStatus.Created:
                        _logger.LogInformation("Guest added: {GuestId}", guest.Id);
                        return Created($"/Guest/{guest.Id}", guest.Id);
                    case GuestStatus.Exists:
                        return Ok("Duplicate Guest Info, Guest Already Exists");
                    default:
                        return BadRequest("Error Occured while saving guest Information");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"GuestController -> AddGuest -> Guest Add operation Failed. Exception:- {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Gets all the guests information for the authenticated user.
        /// </summary>
        /// <returns>List of guests object, on failure returns appropriate message.</returns>
        [HttpGet("AllGuests")]
        public async Task<ActionResult<List<GuestDto>>> GetAllGuests()
        {
            try
            {
                var guests = await _guestRepository.GetAllGuests(GetUserId());
                if (guests is null || !guests.Any())
                    return Ok("No guests exists. Please add guest information and try again");
                return Ok(guests);
            }
            catch (Exception ex)
            {
                _logger.LogError($"GuestController -> GetAllGuests -> Guest fetch operation Failed. Exception:- {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Gets guest information based on the guest id provided.
        /// </summary>
        /// <param name="guid">unique GUID identifier of the guest entity.</param>
        /// <returns>Guest information if guest id is found, else returns appropriate message.</returns>
        [HttpGet("{guid}")]
        public async Task<ActionResult<GuestDto>> GetGuest(Guid guid)
        {
            try
            {
                var guest = await _guestRepository.GetGuest(GetUserId(), guid);
                if (guest is null || string.IsNullOrWhiteSpace(guest.Email))
                    return Ok("Guest not found.");
                return Ok(guest);
            } 
            catch (Exception ex)
            {
                _logger.LogError($"GuestController -> GetAllGuests -> Guest fetch operation Failed. Exception:- {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
        /// <summary>
        /// Add a uniques phone number for guest entity.
        /// </summary>
        /// <param name="id">Guest Id</param>
        /// <param name="contact">Guest Contact Information</param>
        /// <returns>Guest object with updated phone numbers, on failure returns appropriate message.</returns>
        [HttpPost("{id}/addphone")]
        public async Task<IActionResult> AddPhone(Guid id, [FromBody] GuestContactDto contact)
        {
            try
            {
                var guest = await _guestRepository.GetGuest(GetUserId(), id);
                if (guest == null)
                {
                    return NotFound();
                }

                // Validation to ensure phone number is not duplicate
                var isContactNoTaken = await _guestRepository.CheckDuplicateContact(new List<string> { contact.PhoneNumber });
                if (isContactNoTaken)
                {
                    return BadRequest("Phone number already exists for this guest.");
                }

                if(!contact.PhoneNumber.ValidatePhoneNumber(contact.CountryValue))
                {
                    return BadRequest("Not a valid phone number.");
                }
                guest.PhoneNumbers.Add(contact.PhoneNumber);
                await _guestRepository.Update(guest);

                // Logging
                _logger.LogInformation("Phone added for Guest {GuestId}: {PhoneNumber}", id, contact.PhoneNumber);

                return Ok(guest);
            }
            catch (Exception ex)
            {
                _logger.LogError($"GuestController -> AddPhone -> Add Phone operation Failed. Exception:- {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }


        private Guid GetUserId()
        {
            return ((UserDto?)Request?.HttpContext?.Items["User"])?.Id ?? Guid.Empty;
        }
    }
}