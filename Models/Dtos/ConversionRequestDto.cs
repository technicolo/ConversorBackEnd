namespace ConversorDeMonedasBack.Models.Dtos
{
    public class ConversionRequestDto
    {
        public int UserId { get; set; }
        public int SourceCurrencyId { get; set; }
        public int TargetCurrencyId { get; set; }
        public decimal OriginalAmount { get; set; }
    }
}
