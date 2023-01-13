using Dapper;
using ReceptWebAPI.Models.DTO;
using ReceptWebAPI.Repository.Interfaces;
using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Mvc;

namespace ReceptWebAPI.Repository.Repositories
{
    public class CustomerRepo : ICustomerRepo
    {
        private readonly string _connString;
        public CustomerRepo(IConfiguration config)
        {
            _connString = config.GetConnectionString("ReceptDB");
        }
        public string InsertCustomer(CustomerInsertInputDTO customerInputDto )
        {
            using (IDbConnection conn = new SqlConnection(_connString))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@CustomerName",customerInputDto.CustomerName );
                parameters.Add("@Email", customerInputDto.Email);
                parameters.Add("@Password", customerInputDto.Password);

                var success = conn.Execute("sp_insert_customer", parameters, commandType: CommandType.StoredProcedure);

                if(success > 0)
                {
                    return "New User is Inserted";
                }

                return "something went wrong";

            }
        }

        public CustomerResponseDto LoginCustomer(CustomerLoginInputDTO loginInputDTO)
        {
            using (IDbConnection conn = new SqlConnection(_connString))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Email", loginInputDTO.Email);
                parameters.Add("@Password", loginInputDTO.Password);
                
                var customer = conn.QueryFirst<CustomerResponseDto>("sp_getcustomer_email_pass" +
                    "word", parameters, commandType: CommandType.StoredProcedure);
                return customer;
                
            }
        }

        public string UpdateCustomer(CustomerInsertInputDTO updateinfo, int id)
        {
            using (IDbConnection conn = new SqlConnection(_connString))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@CustomerId", id);
                parameters.Add("@CustomerName", updateinfo.CustomerName);
                parameters.Add("@Email", updateinfo.Email);
                parameters.Add("@Password", updateinfo.Password);

                var success = conn.Execute("sp_customer_update", parameters, commandType: CommandType.StoredProcedure);

                if (success > 0)
                {
                    return "User information is updated";
                }

                return "something went wrong";
            }
        }

        public string DeleteCustomer(int id)
        {
            using (IDbConnection conn = new SqlConnection(_connString))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@CustomerId", id);
                
                var success = conn.Execute("sp_customer_available_status", parameters, commandType: CommandType.StoredProcedure);

                if (success > 0)
                {
                    return "customer is deleted from the database";
                }

                return "something went wrong";
            }
        }


    }
}
