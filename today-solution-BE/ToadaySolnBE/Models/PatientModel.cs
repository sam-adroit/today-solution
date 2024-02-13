namespace ToadaySolnBE.models
{
    public class PatientModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public decimal Balance { get; set; }
        public DateTime LastPayment { get; set; } = DateTime.Now;
        public int deletePatient { get; set; }  
        public DateTime? created_on { get; set; }
    }
}
