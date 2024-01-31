﻿
using AgendaApi.Models;
using ConverContext.Data;
using ConversorAPI.Models.Enum;
using ConversorBackEnd.entityes;
using ConversorBackEnd.Models;
using ConversorBackEnd.Models.Dtos;
using ConversorBackEnd.Models.Enum;
using static ConversorBackEnd.services.interfaces.IUserServices;

namespace ConversorBackEnd.Services.Implementations
{
    public class UserService : IUserService
    {
        private ConversorContext _context;
        public UserService(ConversorContext context)
        {
            _context = context;
        }
        public GetUserByIdDto? GetById(int userId)
        {
            var user = _context.UserEntitys.SingleOrDefault(u => u.Id == userId);
            if(user is not null) {
                return  new GetUserByIdDto()
                {
                    LastName = user.LastName,
                    FirstName = user.FirstName,
                    UserName = user.UserName,
                    State = user.State,
                    Id = user.Id,
                    Role = user.Role
                };
            }
            return null;
        }

        public UserEntity? ValidateUser(AuthenticationRequestDto authRequestBody)
        {
            return _context.UserEntitys.FirstOrDefault(p => p.UserName == authRequestBody.UserName && p.Password == authRequestBody.Password);
        }

        public List<UserDto> GetAll()
        {
            //Acá hacemos un select para convertir todas las entidades User a GetUserByIdDto para no mandar todos los Contacts de cada user ni tampoco la contraseña y solo enviar la info básica del usuario.
            return _context.UserEntitys.Select(u => new UserDto()
            {
                FirstName = u.FirstName,
                LastName = u.LastName,  
                Id = u.Id,
                Role = u.Role,
                State = u.State,
                UserName = u.UserName
            }).ToList();
        }

        public void Create(CreateAndUpdateUserDto dto)
        {
            UserEntity newUserentity = new UserEntity()
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Password = dto.Password,
                UserName = dto.UserName,
                State = State.Active,
                Role = Role.User,
            };
            _context.UserEntitys.Add(newUserentity);
            _context.SaveChanges();
        }

        //El update funciona de la siguiente manera:
        /*
         * Primero traemos la entidad de la base de datos.
         * Cuando traemos la entidad entity framework trackea las propiedades del objeto
         * Cuando modificamos algo el estado de la entidad pasa a "Modified"
         * Una vez hacemos _context.SaveChanges() esto va a ver que la entidad fue modificada y guarda los cambios en la base de datos.
         */
        public void Update(CreateAndUpdateUserDto dto, int userId)
        {
            UserEntity userToUpdate = _context.UserEntitys.First(u => u.Id == userId);
            userToUpdate.FirstName = dto.FirstName;
            //userToUpdate.UserName = dto.NombreDeUsuario; //Esto no deberíamos actualizarlo, lo mejor es crear un dto para actualización que no contenga este campo.
            userToUpdate.LastName = dto.LastName;
            userToUpdate.Password = dto.Password;
            _context.SaveChanges();
        }

        public void RemoveUser(int userId)
        {
            var user = _context.UserEntitys.SingleOrDefault(u => u.Id == userId);
            if (user is null)
            {
                throw new Exception("El cliente que intenta eliminar no existe");
            }

            if (user.FirstName != "Admin")
            {
                Delete(userId);
            }
            else
            {
                Archive(userId);
            }
        }

        private void Delete(int id)
        {
            _context.UserEntitys.Remove(_context.UserEntitys.Single(u => u.Id == id));
            _context.SaveChanges();
        }

        private void Archive(int id)
        {
            UserEntity? user = _context.UserEntitys.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                user.State = State.Archived;
            }
            _context.SaveChanges();
        }

        public bool CheckIfUserExists(int userId)
        {
            UserEntity? user = _context.UserEntitys.FirstOrDefault(user => user.Id == userId);
            return user != null;
        }
    }
}
