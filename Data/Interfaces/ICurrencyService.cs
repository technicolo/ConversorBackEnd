using ConversorDeMonedasBack.Entities;
using ConversorDeMonedasBack.Models.Dtos;

namespace ConversorDeMonedasBack.Data.Interfaces
{
    public interface ICurrencyService
    {
        List<Currency> GetAllCurrencies();
        Currency? GetCurrencyById(int currencyId);
        Currency? GetCurrencyByName(string currencyName);
        void CreateCurrency(CreateAndUpdateCurrencyDto dto);
        void UpdateCurrency(CreateAndUpdateCurrencyDto dto, int currencyId);
        void DeleteCurrency(int currencyId);
        bool CheckIfCurrencyExists(int currencyId);

        List<Currency> GetFavouriteCurrencies(int userId);
        void AddFavouriteCurrency(int userId, int currencyId);
        void DeleteFavouriteCurrency(int userId, int currencyId);
    }
}
