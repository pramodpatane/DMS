using DMS.Server.Domain.Entities.Core;
using Server.Application.Interfaces;
using Server.Domain.Entities.Core;
using Server.Domain.Models.Core;
using Server.Infrastructure.DAL.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace Server.Application.Services.Core
{
    public class AuthService : IAuthService
    {
        private readonly IAuthDAL _authDAL;
        private readonly IJWTTokenService _jwtService;
        public AuthService(IAuthDAL authDAL, IJWTTokenService jwtService)
        {
            _authDAL = authDAL;
            _jwtService = jwtService;
        }

        /// <summary>
        /// This service method to Login user
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<LoginResponse> Login(Login login)
        {
            try
            {
                var response = new LoginResponse();
                var userExistRes = await _authDAL.IsUserExist(login.Useremail);

                if (Convert.ToInt32(userExistRes) == 2)
                {
                    response.IsSuccess = false;
                    response.Message = "User EmailId is not verified.";
                    return response;
                }

                if (Convert.ToInt32(userExistRes) == 0)
                {
                    response.IsSuccess = false;
                    response.Message = "Invalid email or password.";
                    return response;
                }

                if (Convert.ToInt32(userExistRes) == 1)
                {
                    var user = await _authDAL.GetUserByEmail(login.Useremail);

                    if (user == null)
                        throw new Exception("Invalid email or password.");

                    if (!login.IsOtpVerified)
                    {
                        if (!VerifyPassword(login.Password, user.PasswordHash, user.PasswordSalt))
                            throw new Exception("Invalid email or password.");
                    }

                    var token = _jwtService.GenerateToken(user);

                    response.Token = token.Token;
                    response.RefreshToken = token.RefreshToken;
                    response.ExpiresIn = token.Expires;
                    response.Id = user.Id;
                    response.RecordId = user.RecordId;
                    response.Email = user.Email;
                    response.UserName = user.UserName;
                    response.RoleName = user.RoleName;
                    response.IsActive = user.IsActive;
                    response.ProfilePhotoUrl = user.ProfilePhotoUrl;

                    response.IsSuccess = true;
                    response.Message = "Login Succeed!";
                }

                return response;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Sends a password reset request for the specified email address. 
        /// </summary>
        /// <param name="Email">The email address of the user requesting a password reset. Cannot be null or empty.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a Response object indicating
        /// whether the password reset request was successful. If the email is not registered or not verified, the
        /// response will indicate failure with an appropriate message.</returns>
        /// <exception cref="Exception">Thrown if an error occurs while processing the password reset request.</exception>
        public async Task<Response> SendResetPassword(string Email)
        {
            try
            {
                var response = new Response();
                var userExistRes = await _authDAL.IsUserExist(Email);

                if (Convert.ToInt32(userExistRes) == 2)
                {
                    response.IsSuccess = false;
                    response.Message = "User EmailId is not verified.";
                    return response;
                }

                else if (Convert.ToInt32(userExistRes) == 0)
                {
                    response.IsSuccess = false;
                    response.Message = "Invalid email or password.";
                    return response;
                }

                else if (Convert.ToInt32(userExistRes) == 1)
                {
                    PasswordResetToken model = new PasswordResetToken();
                    response = await InsertResetPassword(model);
                }

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Creates a reset password request for the specified login and returns the result of the operation.
        /// </summary>
        /// <param name="login">The login information for which to create a reset password request. Cannot be null.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a Response object indicating the
        /// outcome of the reset password request.</returns>
        /// <exception cref="Exception">Thrown if an error occurs while creating the reset password request.</exception>
        public async Task<Response> InsertResetPassword(PasswordResetToken model)
        {
            try
            {
                var response = await _authDAL.InsertResetPassword(model);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// This service method to verify password salt and hash with DB stored
        /// </summary>
        /// <param name="password"></param>
        /// <param name="storedHash"></param>
        /// <param name="storedSalt"></param>
        /// <returns></returns>
        private bool VerifyPassword(string password, byte[] storedHash, byte[] storedSalt)
        {
            using var hmac = new HMACSHA512(storedSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            return computedHash.SequenceEqual(storedHash);
        }
    }
}
