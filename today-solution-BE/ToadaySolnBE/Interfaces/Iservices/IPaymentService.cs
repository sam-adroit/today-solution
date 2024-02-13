using ToadaySolnBE.DTO;
using ToadaySolnBE.models;
using ToadaySolnBE.Models;
using ToadaySolnBE.Utils;

namespace ToadaySolnBE.interfaces.iservices
{
    public interface IPaymentService
    {
        Task<bool> addPayment(PaymentDTO paymentModel);
        Task<PatientPaymentsDTO> getPaymentsByPatientId(int id, Pagination pag);
        Task<PatientPaymentsDTO> getPatientPaymentsByDateRange(int id, Pagination pag, DateRange dateRange);
    }
}
