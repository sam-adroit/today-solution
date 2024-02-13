using ToadaySolnBE.models;
using ToadaySolnBE.Utils;

namespace ToadaySolnBE.Models
{
    public class PatientPaymentsModel
    {
        public List<PaymentSmModel> Payment { get; set; }
        public PatientSmModel Patient { get; set; }
        public PaginationModel Pagination { get; set; }
    }
}
