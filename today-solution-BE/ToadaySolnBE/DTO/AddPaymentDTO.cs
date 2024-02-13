namespace ToadaySolnBE.dto
{
    public class AddPaymentDTO
    {
        public int Patient_id { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        public DateTime Payment_date { get; set; }
    }
}
