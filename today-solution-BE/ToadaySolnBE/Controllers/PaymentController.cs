using Microsoft.AspNetCore.Mvc;
using ToadaySolnBE.DTO;
using ToadaySolnBE.interfaces.iservices;
using ToadaySolnBE.Utils;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ToadaySolnBE.Controllers
{
    [Route("api/Payment")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        public PaymentController(IPaymentService paymentService) {
            _paymentService = paymentService; 
        }
        // GET: api/<PaymentController>
        [HttpGet("GetPaymentsByPatientId/{id}")]
        public async Task<IActionResult> GetPaymentsByPatientId(int id,[FromQuery] Pagination pag)
        {
            var response = await _paymentService.getPaymentsByPatientId(id, pag);
            return Ok(ResponseDTO<PatientPaymentsDTO>.Success(response));
        }

        // GET api/<PaymentController>/5
        [HttpGet("GetPaymentsByDateRange/{id}")]
        public async Task<IActionResult> GetPaymentsByDateRange(int id, [FromQuery] Pagination pag, [FromQuery] DateRange dateRange)
        {
            var response = await _paymentService.getPatientPaymentsByDateRange(id,pag,dateRange);
            return Ok(ResponseDTO<PatientPaymentsDTO>.Success(response));
        }

        // POST api/<PaymentController>
        [HttpPost("pay")]
        public async Task<IActionResult> AddPayment([FromBody] PaymentDTO paymentDTO)
        {
            var response = await _paymentService.addPayment(paymentDTO);
            return Ok(ResponseDTO<bool>.Success(response));
        }
    }
}
