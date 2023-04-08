using ReceitasDeFamilia.Models;
using ReceitasDeFamilia.Repositories;
using System.Reflection;

namespace ReceitasDeFamilia.Services
{
    public interface ILoginService
    {
        TokenResponse? Login(LoginViewModel model);
    }
    public class LoginService : ILoginService
    {
        private readonly IUserRepository _userRepository;
        public LoginService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public TokenResponse? Login(LoginViewModel model)
        {
            var user = _userRepository.Get(model.Email);

            if (user == null || !user.Email.Equals(model.Email) || user.IsDeleted)
            {
                return null;
            }

            var passwordMatch = SaltService.VerifyPassword(model.Senha, user.Senha, user.PasswordSalt);

            if (!passwordMatch)
            {
                return null;
            }

            var token = TokenService.GenerateToken(user);

            user.Senha = null;
            user.PasswordSalt = null;

            return new TokenResponse
            {
                User = new LoginViewModel
                {
                    Nome = user.Nome,
                    Email = user.Email
                },
                Token = token
            };
        }
    }
}
