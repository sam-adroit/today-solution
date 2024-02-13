using AutoMapper;
using ToadaySolnBE.dto;
using ToadaySolnBE.DTO;
using ToadaySolnBE.models;
using ToadaySolnBE.Models;

namespace ToadaySolnBE.Mapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<PatientModel, PatientDTO>();
            CreateMap<PatientDTO, PatientModel>();
            CreateMap<GetPatientsDTO, PatientModel>(); 
            CreateMap<PatientModel, GetPatientsDTO>(); 
            CreateMap<ListResponseDTO<PatientDTO>, ListResponseModel<PatientModel>>();
            CreateMap<ListResponseModel<PatientModel>, ListResponseDTO<PatientDTO>>();
            CreateMap<PaymentDTO, PaymentModel>();
            CreateMap<PaymentModel, PaymentDTO>();
            CreateMap<PatientPaymentsDTO, PatientPaymentsModel>();
            CreateMap<PatientPaymentsModel, PatientPaymentsDTO>();
            CreateMap<UpdatePatientDTO, PatientModel>();
            CreateMap<PatientModel, UpdatePatientDTO>();
            CreateMap<PatientSmModel, PatientModel>();
            CreateMap<PatientModel, PatientSmModel>();
        }
    }
}
