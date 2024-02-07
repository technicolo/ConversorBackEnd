using ConversorDeMonedasBack.Data.Interfaces;
using ConversorDeMonedasBack.Entities;
using ConversorDeMonedasBack.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace ConversorDeMonedasBack.Data.Implementations
{
    public class ConversionService: IConversionService
    {
        private readonly CurrencyConverterContext _context; 
        public ConversionService(CurrencyConverterContext context)
        {
            _context = context;
        }

        public List<Conversion> GetAllConversions(int userId)
        {
            return _context.Conversions.Where(c => c.UserId == userId).ToList();
        }
        public void CreateConversion(CreateConversionDto dto)
        {
            Conversion conversion = new Conversion()
            {
                UserId = dto.UserId,
                SourceCurrencyName = dto.SourceCurrencyName,
                TargetCurrencyName = dto.TargetCurrencyName,
                OriginalAmount = dto.OriginalAmount,
                ConvertedAmount = dto.ConvertedAmount,
                Date = DateTime.Now
            };
            _context.Conversions.Add(conversion);
            _context.SaveChanges();
        }
        //Contador de conversiones por mes 
        public int ConversionCounter(int userId)
        {
            DateTime today = DateTime.Today;

            // el primer día del mes actual
            DateTime firstDayOfMonth = new DateTime(today.Year, today.Month, 1);

            // el último día del mes actual
            DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            int conversionCount = _context.Conversions.Count(c => c.UserId == userId && c.Date >= firstDayOfMonth && c.Date <= lastDayOfMonth);

            return conversionCount;
        }
    }
}
