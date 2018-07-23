using Lib.Entities;
using Lib.Repositories;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Lib.Services
{
    public class AdminUserService
    {
        public AdminUser GetByEmail(string email)
        {
            return new AdminUserRepository().GetUserByEmail(email);        }

        public List<AdminUser> GetUsers => new AdminUserRepository().Get().ToList();

        public AdminUser Get(int id) => new AdminUserRepository().GetById(id);

        //public ClaimsIdentity CriarIdentity(UsuarioAdminDto usuario)
        //{
        //    var userManager = new UserManager<UsuarioIdentity>(new UsuarioUserStore(this));
        //    var identity = userManager.CreateIdentity(usuario.MapTo<UsuarioIdentity>(), DefaultAuthenticationTypes.ApplicationCookie);
        //    return identity;
        //}

        public string CriptografarSenha(string pass)
        {
            SHA256CryptoServiceProvider cryptoTransform = new SHA256CryptoServiceProvider();
            string hash = BitConverter.ToString(cryptoTransform.ComputeHash(Encoding.Default.GetBytes(pass))).Replace("-", "");
            return hash.ToLower();
        }

        private bool VerificarSenha(string hash, string senha)
        {
            return (hash == CriptografarSenha(senha));
        }
    }
}
