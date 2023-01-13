using ReceptWebAPI.Models.Domain;
using ReceptWebAPI.Models.DTO;
using ReceptWebAPI.Repository.Interfaces;
using System.Data.SqlClient;
using System.Data;
using Dapper;

namespace ReceptWebAPI.Repository.Repositories
{
    public class ReceptRepo : IReceptRepo
    {
        private readonly string _connString;
        public ReceptRepo(IConfiguration configuration)
        {
            _connString = configuration.GetConnectionString("ReceptDB");
        }
        public List<ReceptResponseDto> GetAllRecepts()
        {
            using (IDbConnection conn = new SqlConnection(_connString))
            {
                var recepts = (List<ReceptResponseDto>)conn.Query<ReceptResponseDto>("sp_getall_recepts", commandType: CommandType.StoredProcedure);
                if (recepts != null)
                {
                    return recepts;
                }

                return null;

            }
        }
        public Recept GetIndividualReceptById(int receptId)
        {
            using (IDbConnection conn = new SqlConnection(_connString))
            {
                DynamicParameters parameters= new DynamicParameters();
                parameters.Add("@ReceptId", receptId);
                var recept = conn.QuerySingle<Recept>("sp_getrecept_byReceptId",parameters, commandType: CommandType.StoredProcedure);
                if (recept != null)
                {
                    return recept;
                }

                return null;

            }
        }
        public List<ReceptResponseDto> GetAvailableReceptsByCustomerId(int customerId)
        {
            using (IDbConnection conn = new SqlConnection(_connString))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@CustomerId", customerId);
                var recepts = (List<ReceptResponseDto>)conn.Query<ReceptResponseDto>("sp_getrecepts_byCustomerId", parameters, commandType: CommandType.StoredProcedure);
                if (recepts != null)
                {
                    return recepts;
                }

                return null;

            }
        }
        public ReceptStatusResponseDto InsertReceptByCustomerId(ReceptInputInsertDto receptInputDto, int customerId)
        {
            using (IDbConnection conn = new SqlConnection(_connString))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Title", receptInputDto.Title);
                parameters.Add("@Description", receptInputDto.Description);
                parameters.Add("@Ingredients", receptInputDto.Ingredients);
                parameters.Add("@CategoryId", receptInputDto.CategoryId);
                parameters.Add("@CustomerId", customerId);

                ReceptStatusResponseDto response = (ReceptStatusResponseDto)conn.QuerySingle<ReceptStatusResponseDto>("sp_insertrecept_byCustomerId", parameters, commandType: CommandType.StoredProcedure);

                if (response != null)
                {
                    return response;
                }

                return null;

            }
        }

        public ReceptStatusResponseDto UpdateReceptById(ReceptUpdateDto updateinfo, int customerId, int receptId)
        {
            using (IDbConnection conn = new SqlConnection(_connString))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Title", updateinfo.Title);
                parameters.Add("@Description", updateinfo.Description);
                parameters.Add("@Ingredients", updateinfo.Ingredients);
                parameters.Add("@ReceptId", receptId);
                parameters.Add("@CustomerId", customerId);

                ReceptStatusResponseDto response = (ReceptStatusResponseDto)conn.QuerySingle<ReceptStatusResponseDto>("sp_update_recept_byid", parameters, commandType: CommandType.StoredProcedure);

                if (response != null)
                {
                    return response;
                }

                return null;

            }
        }
        public string DeleteReceptById(int customerId, int receptId)
        {
            using (IDbConnection conn = new SqlConnection(_connString))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@CustomerId",customerId);
                parameters.Add("@ReceptId", receptId);

                var success = conn.Execute("sp_delete_recept_byid", parameters, commandType: CommandType.StoredProcedure);
                if(success > 0)
                {
                    return "Recept is deleted";
                }

                return "Some thing went wrong";

            }
        }

        public string SetRatingValue(int customerId, int receptId,int ratingValue)
        {
            using (IDbConnection conn = new SqlConnection(_connString))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@CustomerId", customerId);
                parameters.Add("@ReceptId", receptId);
                parameters.Add("@Rating", ratingValue);

                var response = conn.Execute("sp_update_rating_byId", parameters, commandType: CommandType.StoredProcedure);

                if (response > 0)
                {
                    return "Rating value is updated";
                }

                return "Something went wrong!!";

            }
        }

        public ReceptResponseDto GetReceptByTitle(string title)
        {
            using (IDbConnection conn = new SqlConnection(_connString))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Title", title);
                var recept = (ReceptResponseDto)conn.QuerySingle<ReceptResponseDto>("sp_serach_recept_bytitle", parameters, commandType: CommandType.StoredProcedure);
                if (recept != null)
                {
                    return recept;
                }

                return null;

            }
        }
    }
}
