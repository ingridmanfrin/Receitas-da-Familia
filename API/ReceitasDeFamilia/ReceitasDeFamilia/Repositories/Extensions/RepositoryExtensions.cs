using ReceitasDeFamilia.Models;
using ReceitasDeFamilia.Repositories.Entities;
using ReceitasDeFamilia.Services;

namespace ReceitasDeFamilia.Repositories.Extensions
{
    public static class RepositoryExtensions
    {
        public static UserViewModel ToUserViewModel(this Usuario user)
        {
            if (user == null)
            {
                return null;
            }

            return new UserViewModel()
            {
                UserId = user.IdUsuario,
                Nome = user.Nome,
                Email = user.Email,
                IsEmailValidated = user.EmailValidado,
                Senha = user.Senha,
                PasswordSalt = user.Salt,
                IsDeleted = user.FoiDeletado,
                CreatedDatetime = user.DataCriacao,
                LastEditDatetime = user.DataAlteracao,
                UsuarioAlteracao = user.UsuarioAlteracao,
                UsuarioCriacao = user.UsuarioCriacao
            };
        }

        public static Usuario ToUserEntity(this UserViewModel user)
        {
            if (user == null)
            {
                return null;
            }

            return new Usuario()
            {
                IdUsuario = user.UserId,
                Nome = user.Nome,
                Email = user.Email,
                EmailValidado = user.IsEmailValidated ?? false,
                Senha = user.Senha,
                Salt = user.PasswordSalt,
                FoiDeletado = user.IsDeleted,
                DataCriacao = user.CreatedDatetime,
                DataAlteracao = user.LastEditDatetime,
                UsuarioCriacao = user.UsuarioCriacao,
                UsuarioAlteracao = user.UsuarioAlteracao
            };
        }
        public static Usuario UpdateFrom(this Usuario user, UserViewModel model)
        {
            if (model == null)
            {
                return user;
            }

            if (user == null)
            {
                user = new Usuario();
            }

            user.DataAlteracao = model.LastEditDatetime;
            user.UsuarioAlteracao = model.UsuarioAlteracao;
            user.Nome = model.Nome;

            return user;
        }
    }
}
