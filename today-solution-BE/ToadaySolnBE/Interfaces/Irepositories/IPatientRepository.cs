using ToadaySolnBE.dto;
using ToadaySolnBE.models;
using ToadaySolnBE.Models;
using ToadaySolnBE.Utils;

namespace ToadaySolnBE.interfaces.irepositories
{
    public interface IPatientRepository
    {
        Task<PatientModel> getPatientById(int id);
        Task<PatientModel> getPatientByEmail(string email);
        Task<ListResponseModel<PatientModel>> getPatientByName(string name, Pagination pag);
        Task<ListResponseModel<PatientModel>> getPatients(Pagination pag);
        Task<bool> addPatient(PatientModel patient);
        Task<bool> updatePatient(PatientModel patient);
        Task<bool> removePatient(int id);

    }
}
