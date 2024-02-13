using Microsoft.AspNetCore.Identity;
using ToadaySolnBE.interfaces.irepositories;
using ToadaySolnBE.models;
using System.Data.SqlClient;
using System.Configuration;
using ToadaySolnBE.dto;
using ToadaySolnBE.Models;
using ToadaySolnBE.Utils;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ToadaySolnBE.repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string cs;

        public PatientRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            cs = _configuration.GetConnectionString("DefaultString");
        }
        /* sp_get_all_patient sp_patients_by_name sp_search_patient_date_range */
        public async Task<bool> addPatient(PatientModel patient)
        {
            int result;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("sp_add_patient", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@email", patient.Email);
                cmd.Parameters.AddWithValue("@firstname", patient.FirstName);
                cmd.Parameters.AddWithValue("@lastname", patient.LastName);
                cmd.Parameters.AddWithValue("@created_on", patient.created_on);
                con.Open();
                result = cmd.ExecuteNonQuery();
            }
            return result == 1;
        }

        private PaginationModel pagination(Pagination pagination)
        {
            var paginationModel = new PaginationModel();
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("sp_pagination_patients", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                  
                con.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int totalItem = (int)reader["totalItem"];
                    paginationModel.CurrentPage = pagination.Skip;
                    float num = totalItem;
                    double res = num / pagination.Take;
                    res = Math.Ceiling(res);
                    paginationModel.TotalPage = Convert.ToInt32(res);
                    paginationModel.ItemPerPage = pagination.Take;
                }
            }
            return paginationModel;
        }

        private List<PatientModel> getByParam(object obj, string proc, string val, Pagination pag = null)
        {
            
            var patients = new List<PatientModel>();
            
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand(proc, con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                if(int.TryParse(obj.ToString(), out int id)) cmd.Parameters.AddWithValue(val, id);
                else cmd.Parameters.AddWithValue(val, obj.ToString());
                if(pag != null)
                {
                    cmd.Parameters.AddWithValue("@skip", (pag.Skip -1) * pag.Take);
                    cmd.Parameters.AddWithValue("@take", pag.Take);
                }

                con.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var patient = new PatientModel();
                    patient.Id = Convert.ToInt32(reader["id"]);
                    patient.Email = (string)reader["email"];
                    patient.Balance = (decimal)reader["balance"];
                    patient.FirstName = (string)reader["firstname"];
                    patient.LastName = (string)reader["lastname"];
                    //patient.LastPayment = reader["lastPayment"] != null ? (DateTime)reader["lastPayment"] : new DateTime();
                    patients.Add(patient);
                }
            }
            return patients;
        }

        public async Task<PatientModel> getPatientByEmail(string email)
        {
            return getByParam(email, "sp_get_patient_by_email", "@email").FirstOrDefault();
        }

        public async Task<PatientModel> getPatientById(int id)
        {
            return getByParam(id, "sp_get_patient_by_id", "@id").FirstOrDefault();
        }

        public async Task<ListResponseModel<PatientModel>> getPatientByName(string name, Pagination pag)
        {
            var listResponseModel = new ListResponseModel<PatientModel>();
            var model = getByParam(name, "sp_patients_by_name", "@name", pag);
            var paginationModel = pagination(pag);
            listResponseModel.Pagination = paginationModel;
            listResponseModel.Model = model;
            return listResponseModel;
        }

        public async Task<bool> removePatient(int id)
        {
            var patient = getPatientById(id);
            int result;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("sp_delete_patient", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", patient.Id);
                con.Open();
                result = cmd.ExecuteNonQuery();
            }
            return result == 1;
        }

        public async Task<bool> updatePatient(PatientModel patient)
        {
            int result;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("sp_update_patient_details", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", patient.Id);
                cmd.Parameters.AddWithValue("@email", patient.Email);
                cmd.Parameters.AddWithValue("@firstname", patient.FirstName);
                cmd.Parameters.AddWithValue("@lastname", patient.LastName);
                con.Open();
                result = cmd.ExecuteNonQuery();
            }
            return result == 1; 
        }

        public async Task<ListResponseModel<PatientModel>> getPatients(Pagination pag)
        {
            var listResponseModel = new ListResponseModel<PatientModel>();
            var patients = new List<PatientModel>();
            var paginationModel = pagination(pag);
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("sp_get_all_patients", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@skip", (pag.Skip-1) * pag.Take);
                cmd.Parameters.AddWithValue("@take", pag.Take);
                con.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var patient = new PatientModel();
                    patient.Id = Convert.ToInt32(reader["id"]);
                    patient.Email = (string)reader["email"];
                    patient.Balance = (decimal)reader["balance"];
                    patient.FirstName = (string)reader["firstname"];
                    patient.LastName = (string)reader["lastname"];
                    patient.LastPayment = (DateTime)reader["lastPayment"];
                    patients.Add(patient);
                }
            }
            listResponseModel.Pagination = paginationModel;
            listResponseModel.Model = patients;
            return listResponseModel;
        }
    }
}


/*
                 Reading output value from created or update row
                SqlParameter outP = new SqlParameter();
                outP.ParameterName = "@name"
                outP.SqlDbType = System.Data.SqlDbType.Int;
                outP.Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters.Add(outP)

                 */
