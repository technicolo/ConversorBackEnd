using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConversorDeMonedasBack.Entities
{
    public class Subscription
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [StringLength(50)]
        public string Name { get; set; }
        public int AmountOfConversions { get; set; }
        public string Price { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
