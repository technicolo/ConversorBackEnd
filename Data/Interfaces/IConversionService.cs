using ConversorDeMonedasBack.Entities;
using ConversorDeMonedasBack.Models.Dtos;

namespace ConversorDeMonedasBack.Data.Interfaces
{
    public interface IConversionService
    {
        List<Conversion> GetAllConversions(int userId);
        void CreateConversion(CreateConversionDto dto);
        int ConversionCounter(int userId);
    }
}
