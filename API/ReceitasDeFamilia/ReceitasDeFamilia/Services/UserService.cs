using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace ReceitasDeFamilia.Services
{
    /// <summary>
    /// This Class handle the authenticated user and its Claims
    /// </summary>
    public interface IUserService
    {
        string GetName();
        int GetId();
        string GetEmail();
        string GetIdentificacao();
    }
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetEmail()
        {
            var result = string.Empty;

            if (_httpContextAccessor.HttpContext != null)
            {
                result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
            }
            return result;
        }

        public int GetId()
        {
            int result = 0;

            if (_httpContextAccessor.HttpContext != null)
            {
                var strId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                int.TryParse(strId, out result);
            }
            return result;
        }

        public string GetName()
        {
            var result = string.Empty;

            if (_httpContextAccessor.HttpContext != null)
            {
                result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            }
            return result;
        }

        public string GetIdentificacao()
        {
            var result = string.Empty;

            if (_httpContextAccessor.HttpContext != null && GetId() > 0 && !GetEmail().IsNullOrEmpty())
            {
                result = $"{GetId()} - {GetEmail()}";
            }
            return result;
        }
    }
}
