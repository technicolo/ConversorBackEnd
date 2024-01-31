
using ConversorAPI.Models.Enum;
using ConversorBackEnd.Models.Enum;

namespace ConversorBackEnd.Models
{
    public class GetUserByIdDto //Acá usamos un dto que creamos para esta consulta, ya que no queremos que nos quede User -> Contact -> User -> Contact, etc
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public State State { get; set; }
        public Role Role { get; set; }
    }
}
