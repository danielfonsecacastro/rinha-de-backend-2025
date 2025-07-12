namespace Rinha2025.Backend.Models
{
    public class PaymentsViewModel
    {
        public string? CorrelationId { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? RequestedAt { get; set; }
    }
}
