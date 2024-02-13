using ToadaySolnBE.dto;

namespace ToadaySolnBE.DTO
{
    public class GetPatientsDTO: PatientDTO
    {
        public string Id { get; set; }
        public decimal Balance { get; set; }  
        public DateTime? LastPayment {  get; set; }
    }
}
