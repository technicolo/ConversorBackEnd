using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ConversorDeMonedasBack.Entities
{
    public class Conversion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        [StringLength(100)]
        public string SourceCurrencyName { get; set; } // Nombre de la moneda de origen
        
        [StringLength(100)]
        public string TargetCurrencyName { get; set; }
        public decimal OriginalAmount { get; set; } // Cantidad original
        public decimal ConvertedAmount { get; set; }


    }
}
