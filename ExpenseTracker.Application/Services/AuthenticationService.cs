using AutoMapper;
using ExpenseTracker.Application.Interfaces;
using ExpenseTracker.Application.Models;
using ExpenseTracker.Infrastructure.Interfaces;
using ExpenseTracker.Infrastructure.Models;

namespace ExpenseTracker.Application.Services
{
    public class AuthenticationService: IAuthService
    {
        private readonly IAuthRepository _authenticateRepository;
        private readonly IMapper _mapper;
        public AuthenticationService(IAuthRepository authenticateRepository, IMapper mapper)
        {
            this._authenticateRepository = authenticateRepository;
            this._mapper = mapper;
        }

        public async Task<AuthToken> Login(UserDTO model)
        {
            var user = _mapper.Map<User>(model);
            return await _authenticateRepository.Login(user);
        }

        public async Task<Response> Register(UserDTO model)
        {
            var user = _mapper.Map<User>(model);
            return await _authenticateRepository.Register(user);
        }
    }
}