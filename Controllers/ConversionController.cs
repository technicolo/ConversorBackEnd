using ConversorDeMonedasBack.Data.Implementations;
using ConversorDeMonedasBack.Data.Interfaces;
using ConversorDeMonedasBack.Entities;
using ConversorDeMonedasBack.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ConversorDeMonedasBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ConversionController : ControllerBase
    {
        private readonly ICurrencyService _currencyService;
        private readonly IUserService _userService;
        private readonly IConversionService _conversionService;
        private readonly ISubscriptionService _subscriptionService;
        public ConversionController(ICurrencyService currencyService, IUserService userService, IConversionService conversionService, ISubscriptionService subscriptionService)
        {
            _currencyService = currencyService;
            _userService = userService;
            _conversionService = conversionService;
            _subscriptionService = subscriptionService;
        }

        [HttpGet]
        public IActionResult GetAllConversions()
        {
            int userId = Int32.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            return Ok(_conversionService.GetAllConversions(userId));

        }

        [HttpPost("convert")]
        public IActionResult ConvertCurrency([FromBody] ConversionRequestDto requestDto)
        {
            // Obtener el ID del usuario autenticado
            int authenticatedUserId = Int32.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);

            // Comparar el ID del usuario autenticado con el ID en la solicitud
            if (authenticatedUserId != requestDto.UserId)
            {
                return Forbid("No esta autorizado"); 
            }

            User user = _userService.GetUserById(authenticatedUserId);
            int subscriptionId = user.SubscriptionId;
            int conversionCounter = _conversionService.ConversionCounter(requestDto.UserId);
            Subscription userSubscription = _subscriptionService.GetSubscriptionById(subscriptionId);
            if (userSubscription == null) {
                return BadRequest("Suscripcion no encontrada");
            }
            
            if (conversionCounter < userSubscription.AmountOfConversions)
            {
                // Recupera las tasas de conversión de las monedas desde la base de datos
                Currency sourceCurrency = _currencyService.GetCurrencyById(requestDto.SourceCurrencyId);
                Currency targetCurrency = _currencyService.GetCurrencyById(requestDto.TargetCurrencyId);

                if (sourceCurrency == null || targetCurrency == null)
                {
                    return BadRequest("Monedas no encontradas");
                }

                // Realiza la conversión utilizando el índice de convertibilidad
                decimal convertedAmount = requestDto.OriginalAmount * (sourceCurrency.Value / targetCurrency.Value);
                
                var createDto = new CreateConversionDto
                {
                    UserId = requestDto.UserId,
                    SourceCurrencyId = requestDto.SourceCurrencyId,
                    TargetCurrencyId = requestDto.TargetCurrencyId,
                    OriginalAmount = requestDto.OriginalAmount,     
                    ConvertedAmount = convertedAmount,
                    SourceCurrencyName = sourceCurrency.Name,
                    TargetCurrencyName = targetCurrency.Name
                };

                try
                {
                    _conversionService.CreateConversion(createDto);
                }
                catch (Exception ex)
                {
                    return BadRequest($"Error al hacer la conversion: {ex.Message}");
                }
                return Ok(convertedAmount);
            }
            return Forbid();
        }

        [HttpGet("conversionsAmount")]
        public IActionResult GetAmountOfConversions()
        {
            int userId = Int32.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            int amountOfConversions = _conversionService.ConversionCounter(userId);
            return Ok(amountOfConversions);
        }
    }
}
