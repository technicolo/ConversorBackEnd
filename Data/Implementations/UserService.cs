using ConversorDeMonedasBack.Data.Interfaces;
using ConversorDeMonedasBack.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using ConversorDeMonedasBack.Models.Dtos;
using ConversorDeMonedasBack.Models.Enum;
using Microsoft.AspNetCore.JsonPatch;

namespace ConversorDeMonedasBack.Data.Implementations
{
    public class UserService : IUserService
    {
        private readonly CurrencyConverterContext _context;
        public UserService(CurrencyConverterContext context) 
        {
            _context = context; 
        }
        public User? ValidateUser(AuthenticationRequestBodyDto authRequestBody)
        {
            return _context.Users.FirstOrDefault(p => p.Email == authRequestBody.Email && p.Password  == authRequestBody.Password);
        }
        public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }
        public User GetUserById(int userId)
        {
            return _context.Users.SingleOrDefault(u => u.Id == userId);
        }
        public void CreateUser(CreateUserDto dto)
        {
            User newUser = new User()
            {
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Password = dto.Password,
                Username = dto.Username,
                Role = UserRoleEnum.User
            };
           
            _context.Users.Add(newUser);
            _context.SaveChanges();
        }
        
        //El update funciona de la siguiente manera:
        /*
         * Primero traemos la entidad de la base de datos.
         * Cuando traemos la entidad, entity framework trackea las propiedades del objeto
         * Cuando modificamos algo el estado de la entidad pasa a "Modified"
         * Una vez hacemos _context.SaveChanges() esto va a ver que la entidad fue modificada y guarda los cambios en la base de datos.
         */
        public void UpdateUser(UpdateUserDto dto, int userId)
        {
            User userToUpdate = _context.Users.First(u => u.Id == userId);
            userToUpdate.FirstName = dto.FirstName;   
            userToUpdate.LastName = dto.LastName;
            userToUpdate.Username = dto.Username;
            userToUpdate.SubscriptionId = dto.SubscriptionId;
            _context.SaveChanges();
        }
        public void DeleteUser(int userId)
        {
            _context.Users.Remove(_context.Users.Single(u => u.Id == userId));
            _context.SaveChanges();
        }
        public bool CheckIfUserExists(int userId)
        {
            User? user = _context.Users.FirstOrDefault(user => user.Id == userId);
            return user != null;
        }

        public void UpdateUserSubscription(int userId,int subscriptionId)
        {
            try
            {
                User userToUpdate = _context.Users.FirstOrDefault(u => u.Id == userId);

                if (userToUpdate != null)
                {
                    userToUpdate.SubscriptionId = subscriptionId;
                    _context.SaveChanges();
                }
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"DbUpdateException: {ex.Message}");
                Console.WriteLine($"InnerException: {ex.InnerException?.Message}");
            }
        }

    }
}
