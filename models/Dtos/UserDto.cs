using ConversorAPI.Models.Enum;
using ConversorBackEnd.Models.Enum;

namespace ConversorBackEnd.Models.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public State State { get; set; }
        public Role Role { get; set; }
    }
}
