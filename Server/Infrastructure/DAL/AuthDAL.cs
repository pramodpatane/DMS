using Dapper;
using DMS.Server.Domain.Entities.Core;
using Microsoft.Data.SqlClient;
using Server.Domain.Entities.Core;
using Server.Domain.Models.Core;
using Server.Infrastructure.DAL.Interfaces;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace Server.Infrastructure.DAL
{
    public class AuthDAL : IAuthDAL
    {
        private readonly string _connectionString;
        public AuthDAL(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        /// <summary>
        /// This method to convert password into PasswordHash and PasswordSalt
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordHash"></param>
        /// <param name="passwordSalt"></param>
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;                 // auto-generated salt
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        /// <summary>
        /// To check entered user is exist or not
        /// </summary>
        /// <param name="useremail"></param>
        /// <returns></returns>
        public async Task<int> IsUserExist(string useremail)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@value", "ValidateUser");
                parameters.Add("@userEmail", useremail);

                var newId = await connection.ExecuteScalarAsync<int>(
                    "USP_UserLogin",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );
                return newId;
            }
        }

        /// <summary>
        /// Get user details by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<LoginResponse> GetUserByEmail(string email)
        {
            using var con = new SqlConnection(_connectionString);
            var parameters = new DynamicParameters();
            parameters.Add("@value", "GetUserByEmail");
            parameters.Add("@userEmail", email);

            return await con.QueryFirstOrDefaultAsync<LoginResponse>(
                "USP_UserLogin",
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<Response> InsertResetPassword(PasswordResetToken model)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@value1", "Insert");
                parameters.Add("@recordId", model.RecordId);

                var data = await connection.ExecuteScalarAsync<int>(
                    "USP_ResetPasswordToken",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                return new Response { IsSuccess = data > 0 };
            }
        }
    }
}
