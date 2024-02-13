using ToadaySolnBE.dto;
using ToadaySolnBE.models;
using ToadaySolnBE.Models;
using ToadaySolnBE.Utils;

namespace ToadaySolnBE.interfaces.irepositories
{
    public interface IPaymentRepository
    {
        Task<bool> addPayment(PaymentModel paymentModel);
        Task<PatientPaymentsModel> getPaymentsByPatientId(int id, Pagination pag);
        Task<PatientPaymentsModel> getPatientPaymentsByDateRange(int id, Pagination pag, DateRange dateRange);
    }
}
