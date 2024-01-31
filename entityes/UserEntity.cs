using ConversorBackEnd.Models.Enum;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ConversorAPI.Models.Enum;

namespace ConversorBackEnd.entityes
{
    public class UserEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        [Required]
        public string UserName { get; set; }

        public State State { get; set; } = State.Active;

        public Role Role { get; set; } = Role.User;
    }
}
