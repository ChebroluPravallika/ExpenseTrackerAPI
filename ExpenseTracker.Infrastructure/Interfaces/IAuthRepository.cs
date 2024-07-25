using ExpenseTracker.Infrastructure.Models;

namespace ExpenseTracker.Infrastructure.Interfaces
{
    public interface IAuthRepository
    {
        /// <summary>
        /// Login a user
        /// </summary>
        /// <param name="model">Login credentials</param>
        /// <returns>JWT Token</returns>
        public Task<AuthToken> Login(User model);

        /// <summary>
        /// Create a user
        /// </summary>
        /// <param name="model">user details</param>
        /// <returns>status code</returns>
        public Task<Response> Register(User model);
    }
}
