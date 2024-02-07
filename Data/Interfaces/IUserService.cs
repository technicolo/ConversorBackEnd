using ConversorDeMonedasBack.Entities;
using ConversorDeMonedasBack.Models.Dtos;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;

namespace ConversorDeMonedasBack.Data.Interfaces
{
    public interface IUserService
    {
        User? ValidateUser(AuthenticationRequestBodyDto authRequestBody);
        List<User> GetAllUsers();
        User? GetUserById(int userId);
        void CreateUser(CreateUserDto dto);
        void UpdateUser(UpdateUserDto dto, int userId);
        void DeleteUser(int userId);
        bool CheckIfUserExists(int userId);
        void UpdateUserSubscription(int userId, int subscriptionId);

    }

}
