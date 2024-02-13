using ToadaySolnBE.dto;
using ToadaySolnBE.DTO;
using ToadaySolnBE.models;
using ToadaySolnBE.Utils;

namespace ToadaySolnBE.interfaces.iservices
{
    public interface IPatientService
    {
        Task<GetPatientsDTO> GetPatientById(int id);
        Task<GetPatientsDTO> GetPatientByEmail(string email);
        Task<ListResponseDTO<GetPatientsDTO>> GetPatientByName(string name, Pagination pag);
        Task<ListResponseDTO<GetPatientsDTO>> GetPatients(Pagination pag);
        Task<bool> AddPatient(PatientDTO patient);
        Task<bool> UpdatePatient(UpdatePatientDTO patient);
        Task<bool> RemovePatient(int id);

    }
}
