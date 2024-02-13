using AutoMapper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ToadaySolnBE.dto;
using ToadaySolnBE.DTO;
using ToadaySolnBE.interfaces.irepositories;
using ToadaySolnBE.interfaces.iservices;
using ToadaySolnBE.models;
using ToadaySolnBE.Utils;

namespace ToadaySolnBE.services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;
        public PatientService(IPatientRepository patientRepository,IMapper mapper ) { 
            _patientRepository = patientRepository;
            _mapper = mapper;
        }
        public async Task<bool> AddPatient(PatientDTO patient)
        {
            var patientMap = _mapper.Map<PatientModel>(patient);
            return await _patientRepository.addPatient(patientMap);
        }

        public async Task<GetPatientsDTO> GetPatientByEmail(string email)
        {
            var result = await _patientRepository.getPatientByEmail(email);
            return _mapper.Map<GetPatientsDTO>(result);
        }

        public async Task<GetPatientsDTO> GetPatientById(int id)
        {
            var result = await _patientRepository.getPatientById(id);
            return _mapper.Map<GetPatientsDTO>(result);
        }

        public async Task<ListResponseDTO<GetPatientsDTO>> GetPatientByName(string name, Pagination pag)
        {
            var result = await _patientRepository.getPatientByName(name, pag);
            var resultDTO = _mapper.Map<List<GetPatientsDTO>>(result.Model);
            return new ListResponseDTO<GetPatientsDTO>(resultDTO,result.Pagination);
        }

        public async Task<ListResponseDTO<GetPatientsDTO>> GetPatients(Pagination pag)
        {
            var result = await _patientRepository.getPatients(pag);
            var resultDTO = _mapper.Map<List<GetPatientsDTO>>(result.Model);
            return new ListResponseDTO<GetPatientsDTO>(resultDTO, result.Pagination);
        }

        public async Task<bool> RemovePatient(int id)
        {
            return await _patientRepository.removePatient(id);
        }

        public async Task<bool> UpdatePatient(UpdatePatientDTO patient)
        {
            var patientMap = _mapper.Map<PatientModel>(patient);
            return await _patientRepository.updatePatient(patientMap);
        }
    }
}
