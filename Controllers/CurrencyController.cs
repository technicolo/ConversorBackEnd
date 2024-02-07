using ConversorDeMonedasBack.Data.Interfaces;
using ConversorDeMonedasBack.Entities;
using ConversorDeMonedasBack.Models.Dtos;
using ConversorDeMonedasBack.Models.Enum;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ConversorDeMonedasBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyService _currencyService; 
        public CurrencyController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        [HttpGet]
        public IActionResult GetAllCurrencies()
        {
            return Ok(_currencyService.GetAllCurrencies());
        }

        [HttpGet("{currencyId}")]
        public IActionResult GetCurrencyById(int currencyId)
        {
            if (currencyId == 0)
            {
                return BadRequest();
            }

            Currency? currency = _currencyService.GetCurrencyById(currencyId);

            if (currency is null)
            {
                return NotFound();
            }

            var dto = new GetCurrencyByIdResponse()
            {
                Id = currency.Id,
                Name = currency.Name,
                Symbol = currency.Symbol,
                Value = currency.Value
            };
            return Ok(dto);
        }

        [HttpPost]
        public IActionResult CreateCurrency(CreateAndUpdateCurrencyDto dto)
        {
            string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            if (role == UserRoleEnum.Admin.ToString())
            {
                try
                {
                    _currencyService.CreateCurrency(dto);
                }
                catch (Exception ex)
                {
                    BadRequest(ex);
                }
                return Created("Created", dto);
            }
            return Forbid();
        }

        [HttpPut("{currencyId}")]
        public IActionResult UpdateCurrency(CreateAndUpdateCurrencyDto dto, int currencyId)
        {
            string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            if (role == UserRoleEnum.Admin.ToString())
            {
                if (!_currencyService.CheckIfCurrencyExists(currencyId))
                {
                    return NotFound();
                }
                try
                {
                    _currencyService.UpdateCurrency(dto, currencyId);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }
                return NoContent();
            }
            return Forbid();
        }

        [HttpDelete("{currencyId}")]
        public IActionResult DeleteCurrency(int currencyId)
        {
            string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            if (role == UserRoleEnum.Admin.ToString())
            {
                Currency? currency = _currencyService.GetCurrencyById(currencyId);
                if (currency is null)
                {
                    return NotFound();
                }
                 _currencyService.DeleteCurrency(currencyId);

                return NoContent();
            }
            return Forbid();
        }
        
        #region Monedas Favoritas
        
        [HttpGet("favorite")]
        public IActionResult GetFavoriteCurrencies()
        {

            try
            {
                int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
                if (userId != null)
                {
                    return Ok(_currencyService.GetFavouriteCurrencies(userId));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return BadRequest("La reclamación de identificación del usuario no está presente.");
        }

        [HttpPost("favorite/{currencyId}")]
        public IActionResult AddFavoriteCurrency(int currencyId)
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            if (!_currencyService.CheckIfCurrencyExists(currencyId))
            {
                return NotFound();
            }
            _currencyService.AddFavouriteCurrency(currencyId, userId);
            return Created("Created",currencyId);
        }

        [HttpDelete("favorite/{currencyId}")]
        public IActionResult DeleteFavoriteCurrency(int currencyId)
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            if (!_currencyService.CheckIfCurrencyExists(currencyId))
            {
                return NotFound();
            }
            _currencyService.DeleteFavouriteCurrency(currencyId, userId);
            return NoContent();
        }
        #endregion
    }
}
