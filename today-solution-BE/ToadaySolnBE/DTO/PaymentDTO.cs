using ToadaySolnBE.models;

namespace ToadaySolnBE.DTO
{
    public class PaymentDTO
    {
        public int Patient_Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime Payment_date { get; set; }
    }
}
