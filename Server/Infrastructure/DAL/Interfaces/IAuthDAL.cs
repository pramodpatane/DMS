using DMS.Server.Domain.Entities.Core;
using Server.Domain.Entities.Core;
using Server.Domain.Models.Core;

namespace Server.Infrastructure.DAL.Interfaces
{
    public interface IAuthDAL
    {
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        
        public Task<int> IsUserExist(string useremail);

        public Task<LoginResponse> GetUserByEmail(string email);           
 
        public Task<Response> InsertResetPassword(PasswordResetToken model);
    }
}
