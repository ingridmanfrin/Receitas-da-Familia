using Microsoft.IdentityModel.Tokens;
using ReceitasDeFamilia.Services;

namespace ReceitasDeFamilia.Models
{
    public interface IModelBase
    {
        bool IsDeleted { get; set; }
        DateTime CreatedDatetime { get; set; }
        DateTime LastEditDatetime { get; set; }
    }
    public class ModelBase : IModelBase
    {
        public ModelBase()
        {
            IsDeleted = false;
            CreatedDatetime = DateTime.Now;
            LastEditDatetime = DateTime.Now;
            UsuarioCriacao = string.Empty;
            UsuarioAlteracao = string.Empty;
        }

        public bool IsDeleted { get; set; }

        public DateTime CreatedDatetime { get; set; }

        public DateTime LastEditDatetime { get; set; }

        public string UsuarioCriacao { get; set; }

        public string UsuarioAlteracao { get; set; }

        /// <summary>
        /// This funciton will update the LastEditDatetime to Datetime.Now
        /// </summary>
        public void Update(IUserService userService)
        {
            LastEditDatetime = DateTime.Now;
            UsuarioAlteracao = userService.GetName().IsNullOrEmpty() ? "ADMIN" : userService.GetName();
        }
        public void SetUsuarioCriacaoFromService(IUserService userService)
        {
            UsuarioCriacao = userService.GetName().IsNullOrEmpty() ? "ADMIN" : userService.GetName();
            UsuarioAlteracao = userService.GetName().IsNullOrEmpty() ? "ADMIN" : userService.GetName();
        }
    }
}
