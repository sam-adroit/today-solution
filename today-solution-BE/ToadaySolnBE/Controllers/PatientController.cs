using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ToadaySolnBE.dto;
using ToadaySolnBE.DTO;
using ToadaySolnBE.interfaces.iservices;
using ToadaySolnBE.models;
using ToadaySolnBE.Utils;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ToadaySolnBE.Controllers
{
    [Route("api/Patient")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;
        public PatientController(IPatientService patientService) {
            _patientService = patientService;
        }
        // GET: api/<PatientController>
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _patientService.GetPatientById(id);
            return Ok(ResponseDTO<GetPatientsDTO>.Success(response));
        }

        // GET api/<PatientController>/5
        [HttpGet("GetByEmail")]
        public async Task<IActionResult> GetByEmail([FromQuery] string email)
        {
            var response = await _patientService.GetPatientByEmail(email);
            return Ok(ResponseDTO<GetPatientsDTO>.Success(response));
        }

        [HttpGet("GetByName")]
        public async Task<IActionResult> GetByName(string name, [FromQuery] Pagination pag)
        {
            var response = await _patientService.GetPatientByName(name, pag);
            return Ok(ResponseDTO<ListResponseDTO<GetPatientsDTO>>.Success(response));
        }

        [HttpGet("GetAllPatients")]
        public async Task<IActionResult> GetAll([FromQuery] Pagination pag)
        {
            var response = await _patientService.GetPatients(pag);
            return Ok(ResponseDTO<ListResponseDTO<GetPatientsDTO>>.Success(response));
        }

        // POST api/<PatientController>
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] PatientDTO patient)
        {
            var response = await _patientService.AddPatient(patient);
            return Ok(ResponseDTO<bool>.Success(response));
        }

        // PUT api/<PatientController>/5
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] UpdatePatientDTO patient)
        {
            var response = await _patientService.UpdatePatient(patient);
            return Ok(ResponseDTO<bool>.Success(response));
        }
    }
}
