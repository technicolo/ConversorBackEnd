using ConversorDeMonedasBack.Models.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConversorDeMonedasBack.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [StringLength(50)]
        public string Username { get; set; }
        
        [StringLength(50)]
        public string Email { get; set; }
        
        [StringLength(50)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [StringLength(50)]
        public string FirstName { get; set; }
        
        [StringLength(50)]
        public string LastName { get; set; }
        public ICollection<Currency> Currencies { get; set; } = new List<Currency>();

        [ForeignKey("SubscriptionId")]
        public int SubscriptionId { get; set; } = 10;
        public Subscription Subscription { get; set; }
        public ICollection<Conversion> Conversions { get; set; } = new List<Conversion>();

        public UserRoleEnum Role { get; set; } = UserRoleEnum.User;
    }
}
