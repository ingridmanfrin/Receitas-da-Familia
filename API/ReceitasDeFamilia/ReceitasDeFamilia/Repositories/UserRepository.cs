using ReceitasDeFamilia.Models;
using ReceitasDeFamilia.Repositories.Entities;
using ReceitasDeFamilia.Repositories.Extensions;
using Microsoft.AspNetCore.Components.Server;
using System.Linq;
using ReceitasDeFamilia.Services;

namespace ReceitasDeFamilia.Repositories
{
    public interface IUserRepository
    {
        UserViewModel? Get(string email, string password);
        UserViewModel? Get(string email);
        UserViewModel? Get(int userId);
        UserViewModel Add(UserViewModel model);
        UserViewModel Update(UserViewModel model);
        bool Delete(int userId);
        //string? GetSalt(string username);
    }
    public class UserRepository : IUserRepository
    {

        public UserViewModel? Get(string email, string password)
        {
            using (var context = new ReceitasDeFamiliaDbContext())
            {
                return context.Usuarios.Where(x => x.Email == email && x.Senha == password).FirstOrDefault().ToUserViewModel();
            }
        }

        public UserViewModel? Get(string email)
        {
            using (var context = new ReceitasDeFamiliaDbContext())
            {
                return context.Usuarios.Where(x => x.Email == email).FirstOrDefault().ToUserViewModel();
            }
            //var users = new List<UserViewModel>();
            //users.Add(new UserViewModel { Id = 1, Username = "batman", Password = "79nnpm07rw521WcZ7B9iFnJUxPsTd+rj4JHcODPua+CLE0Tu7x11UStsaAIUJzJ/UZfBFL6FOBl1Uu8acQU35Q==", PasswordSalt = "4Oq3oZr4Y4737N4WutKRkAt113dFg6N3ShO0zqY/CMcMh9Y3ao64Hgvanofxo34KG7cXn7UXBQItdyzX67TRvQ==", Role = "manager" });
            //users.Add(new UserViewModel { Id = 2, Username = "robin", Password = "krrOkuwtxXS1OreZTb7tlDvwamjQTokxEszze+h7/puKtirR4MaX5Q0d/awFIsykM8/AJhveY++K2zNgTpdrlQ==", PasswordSalt = "2AozAhbJDQIe1U4AcHgheXllxgJjx/HkjxWjM+UdZC1cAg1oPaPTyOezJCA/O1IfEhMqQ8rK+MO/uIWHt1YnSw==", Role = "employee" });
            //return users.Where(x => x.Username.ToLower() == username.ToLower()).FirstOrDefault();
        }

        public UserViewModel? Get(int userId)
        {
            using (var context = new ReceitasDeFamiliaDbContext())
            {
                return context.Usuarios.Find(userId)
                //    .Select(x => new UserViewModel()
                //{
                //    UserId = x.UserId,
                //    Email = x.Email,
                //    PasswordDatetime = x.PasswordDatetime,
                //    IsDeleted = x.IsDeleted,
                //    Name = x.Name,
                //    CreatedDatetime = x.CreatedDatetime,
                //    IsEmailValidated = x.IsEmailValidated,
                //    LastEditDatetime = x.LastEditDatetime
                //})
                    .ToUserViewModel();
            }
        }

        public UserViewModel Add(UserViewModel model)
        {
            using (var context = new ReceitasDeFamiliaDbContext())
            {
                var entity = model.ToUserEntity();
                context.Usuarios.Add(entity);
                context.SaveChanges();
                return entity.ToUserViewModel();
            }
        }

        public UserViewModel Update(UserViewModel model)
        {
            using (var context = new ReceitasDeFamiliaDbContext())
            {
                var userFromDB = context.Usuarios.Find(model.UserId);
                if (userFromDB == null)
                {
                    return null;
                }
                userFromDB.UpdateFrom(model);
                context.Usuarios.Update(userFromDB);
                context.SaveChanges();
                return userFromDB.ToUserViewModel();
            }
        }

        public bool Delete(int userId)
        {
            using (var context = new ReceitasDeFamiliaDbContext())
            {
                var userFromDB = context.Usuarios.Find(userId);
                if (userFromDB == null)
                {
                    return false;
                }

                userFromDB.FoiDeletado = true;

                context.Usuarios.Update(userFromDB);
                context.SaveChanges();
                return true;
            }
        }
    }
}
