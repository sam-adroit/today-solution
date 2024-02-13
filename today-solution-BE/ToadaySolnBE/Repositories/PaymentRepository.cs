using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using ToadaySolnBE.dto;
using ToadaySolnBE.interfaces.irepositories;
using ToadaySolnBE.models;
using ToadaySolnBE.Models;
using ToadaySolnBE.Utils;

namespace ToadaySolnBE.repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string cs;

        public PaymentRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            cs = _configuration.GetConnectionString("DefaultString");
        }
        public async Task<bool> addPayment(PaymentModel payment)
        {
            int result;
            var balance = getNewBalance(payment.Patient_Id, payment.Amount);
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("sp_add_payment", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@amount", payment.Amount);
                cmd.Parameters.AddWithValue("@balance", balance);
                cmd.Parameters.AddWithValue("@patient_id", payment.Patient_Id);
                cmd.Parameters.AddWithValue("@payment_date", payment.Payment_date);
                con.Open();
                result = cmd.ExecuteNonQuery();
            }
            var updatedBal = updateBalance(payment.Patient_Id, balance, payment.Payment_date);
            return (result == 1) == updatedBal;
        }

        private bool updateBalance(int id, decimal balance, DateTime date)
        {
            int result;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("sp_update_balance", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@balance", balance);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@payment_date", date);
                con.Open();
                result = cmd.ExecuteNonQuery();
            }
            return result == 1;
        }

        private decimal getNewBalance(int id, decimal amount)
        {
            decimal balance = 0;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("sp_get_new_balance", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@amount", amount);
                cmd.Parameters.AddWithValue("@user_id", id);
                con.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    balance = (decimal)reader["new_balance"];
                }
            }
            return balance;

        }

        private PatientPaymentsModel fetchPaymentByParam(int id, Pagination pag,string sp, DateRange? dateRange = null)
        {
            var patPayModel = new PatientPaymentsModel();
            var paymentList = new List<PaymentSmModel>();
            
            var page = pagination(pag);
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand(sp, con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@patient_id", id);
                cmd.Parameters.AddWithValue("@skip", (pag.Skip - 1) * pag.Take);
                cmd.Parameters.AddWithValue("@take", pag.Take);
                if(dateRange != null)
                {
                    cmd.Parameters.AddWithValue("@start_date", dateRange.StartDate.Date);
                    cmd.Parameters.AddWithValue("@end_date", dateRange.EndDate.Date.AddDays(1).AddMilliseconds(-1));
                }
                con.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var payment = new PaymentSmModel();
                    payment.Id = (int)reader["id"];
                    payment.Amount = (decimal)reader["amount"];
                    payment.Payment_date = (DateTime)reader["payment_date"];
                    paymentList.Add(payment);
                }
            }

            patPayModel.Payment = paymentList;
            patPayModel.Pagination = page;
            return patPayModel;
        }

        private PaginationModel pagination(Pagination pagination)
        {
            var paginationModel = new PaginationModel();
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("sp_pagination_payments", con);
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

        public async Task<PatientPaymentsModel> getPaymentsByPatientId(int id, Pagination pag)
        {
            return fetchPaymentByParam(id, pag,"sp_get_payments_by_patient_id");
        }

        public async Task<PatientPaymentsModel> getPatientPaymentsByDateRange(int id, Pagination pag, DateRange dateRange)
        {
            return fetchPaymentByParam(id, pag, "sp_get_patient_payments_by_date_range", dateRange);
        }
    }
}
