namespace ConversorDeMonedasBack.Models.Dtos
{
    public class CreateConversionDto
    {
        public int UserId { get; set; } // ID del usuario
        public int SourceCurrencyId { get; set; } // ID de la moneda de origen
        public int TargetCurrencyId { get; set; } // ID de la moneda de destino
        public decimal OriginalAmount { get; set; } // Cantidad original
        public decimal ConvertedAmount { get; set; } // Cantidad convertida
        public string SourceCurrencyName { get; set; } // Nombre de la moneda de origen
        public string TargetCurrencyName { get; set; } // Nombre de la moneda de destino
    }

}
