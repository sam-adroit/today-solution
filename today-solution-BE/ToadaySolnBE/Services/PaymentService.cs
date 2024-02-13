using AutoMapper;
using ToadaySolnBE.DTO;
using ToadaySolnBE.interfaces.irepositories;
using ToadaySolnBE.interfaces.iservices;
using ToadaySolnBE.models;
using ToadaySolnBE.Models;
using ToadaySolnBE.Utils;

namespace ToadaySolnBE.services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;
        public PaymentService(IPaymentRepository paymentRepository, IPatientRepository patientRepository, IMapper mapper)
        {
            _paymentRepository = paymentRepository;
            _patientRepository = patientRepository;
            _mapper = mapper;
        }

        public async Task<bool> addPayment(PaymentDTO paymentModel)
        {
            var payment = _mapper.Map<PaymentModel>(paymentModel);
            var result = await _paymentRepository.addPayment(payment);
            return result;
        }

        public async Task<PatientPaymentsDTO> getPatientPaymentsByDateRange(int id, Pagination pag, DateRange dateRange)
        {
            var payment = await _paymentRepository.getPatientPaymentsByDateRange(id, pag, dateRange);
            var result = await _patientRepository.getPatientById(id);
            payment.Patient = _mapper.Map<PatientSmModel>(result);
            return _mapper.Map<PatientPaymentsDTO>(payment);
        }

        public async Task<PatientPaymentsDTO> getPaymentsByPatientId(int id, Pagination pag)
        {
            var payment = await _paymentRepository.getPaymentsByPatientId(id, pag);
            var result = await _patientRepository.getPatientById(id);
            payment.Patient = _mapper.Map<PatientSmModel>(result);
            return _mapper.Map<PatientPaymentsDTO>(payment);
        }
    }
}
