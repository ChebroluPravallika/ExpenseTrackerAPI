using ExpenseTracker.Application.Models;
using ExpenseTracker.Infrastructure.Models;

namespace ExpenseTracker.Application.Interfaces
{
    public interface IAuthService
    {
        /// <summary>
        /// Login a user
        /// </summary>
        /// <param name="model"> Login credentials </param>
        /// <returns>An authentication token if user credentials match</returns>
        public Task<AuthToken> Login(UserDTO model);

        /// <summary>
        /// Create a User
        /// </summary>
        /// <param name="model"> User details </param>
        /// <returns></returns>
        public Task<Response> Register(UserDTO model);
    }
}
