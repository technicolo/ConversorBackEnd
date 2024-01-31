using AgendaApi.Models;
using ConversorBackEnd.entityes;
using ConversorBackEnd.Models;
using ConversorBackEnd.Models.Dtos;

namespace ConversorBackEnd.services.interfaces
{
    public interface IUserServices
    {
        public interface IUserService
        {
            bool CheckIfUserExists(int userId);
            void Create(CreateAndUpdateUserDto dto);
            List<UserDto> GetAll();
            GetUserByIdDto? GetById(int userId);
            void RemoveUser(int userId);
            void Update(CreateAndUpdateUserDto dto, int userId);
            UserEntity? ValidateUser(AuthenticationRequestDto authRequestBody);
        }
    }
}
