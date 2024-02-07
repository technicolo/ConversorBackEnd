using ConversorDeMonedasBack.Data.Interfaces;
using ConversorDeMonedasBack.Entities;
using ConversorDeMonedasBack.Models.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ConversorDeMonedasBack.Data.Implementations
{
    public class CurrencyService : ICurrencyService
    {
        private readonly CurrencyConverterContext _context;
        public CurrencyService(CurrencyConverterContext context)
        {
            _context = context;
        }
        public List<Currency> GetAllCurrencies()
        {
            return _context.Currencies.ToList();
        }
        public Currency GetCurrencyById(int currencyId) 
        {
            return _context.Currencies.SingleOrDefault(c => c.Id == currencyId);
        }
        public Currency GetCurrencyByName(string currencyName) 
        {  
            return _context.Currencies.SingleOrDefault(c => c.Name == currencyName);
        }
        public void CreateCurrency(CreateAndUpdateCurrencyDto dto)
        {
            Currency newCurrency = new Currency()
            {
                Name = dto.Name,
                Symbol = dto.Symbol,
                Value = dto.Value
            };
            _context.Currencies.Add(newCurrency);
            _context.SaveChanges();
        }
        public void UpdateCurrency(CreateAndUpdateCurrencyDto dto, int currencyId)
        {
            Currency currencyToUpdate = _context.Currencies.SingleOrDefault(c => c.Id == currencyId);
            currencyToUpdate.Name = dto.Name;
            currencyToUpdate.Symbol = dto.Symbol;
            currencyToUpdate.Value = dto.Value;
            _context.SaveChanges();
        }
        public void DeleteCurrency(int currencyId)
        {
            _context.Currencies.Remove(_context.Currencies.Single(c => c.Id == currencyId)); 
            _context.SaveChanges();
        }

        public bool CheckIfCurrencyExists(int currencyId)
        {
            Currency? currency = _context.Currencies.FirstOrDefault(currency => currency.Id == currencyId);
            return currency != null;
        }

        #region Monedas Favoritas
        public List<Currency> GetFavouriteCurrencies(int userId)
        {
            // Obtiene el usuario con el id especificado e incluye las monedas favoritas
            User user = _context.Users.Include(u => u.Currencies).FirstOrDefault(u => u.Id == userId);

            // Si el usuario no existe o no tiene monedas favoritas, devuelve una lista vacía
            if (user == null || user.Currencies == null)
            {
                return new List<Currency>();
            }

            // Devuelve las monedas favoritas del usuario, seleccionando solo las propiedades de Currency
            return user.Currencies.Select(c => new Currency
            {
                Id = c.Id,
                Name = c.Name,
                Symbol = c.Symbol,
                Value = c.Value
            }).ToList();
        }

        public void AddFavouriteCurrency(int currencyId, int userId)
        {
            var user = _context.Users.Include(u => u.Currencies).FirstOrDefault(u => u.Id == userId);
            var currency = _context.Currencies.FirstOrDefault(c => c.Id == currencyId);

            if (user != null && currency != null)
            {
                user.Currencies.Add(currency);
                _context.SaveChanges();
            }
        }
        public void DeleteFavouriteCurrency(int currencyId, int userId)
        {
            var user = _context.Users.Include(u => u.Currencies).FirstOrDefault(u => u.Id == userId);
            var currency = _context.Currencies.FirstOrDefault(c => c.Id == currencyId);
            if (user != null && currency != null)
            {
                user.Currencies.Remove(currency);
                _context.SaveChanges();
            }   
        }
        #endregion
    }
}
