namespace ToadaySolnBE.dto
{
    public class PatientPaymentDTO
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int Payment_id { get; set; }
        public decimal Amount { get; set;}
        public decimal Balance { get; set;}
        public DateTime Payment_date { get; set; }


    }
}
