using App.Domain.Entities;
using App.Domain.Repositories;
using App.Services.Dtos;
using App.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace App.Services
{
    public class AdminUserService : IAdminUserService
    {
        private readonly IAdminUserRepository rep;

        public AdminUserService(IAdminUserRepository rep)
        {
            this.rep = rep;
        }

        public AdminUserDto Create(AdminUserDto entity)
        {
            try
            {
                return rep.Create(entity.MapTo<AdminUser>()).MapTo<AdminUserDto>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Delete(AdminUserDto entity)
        {
            try
            {
                return rep.Delete(entity.MapTo<AdminUser>());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public AdminUserDto GetByEmail(string email)
        {
            try
            {
                return rep.GetByEmail(email).MapTo<AdminUserDto>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public AdminUserDto GetById(int id)
        {
            try
            {
                return rep.GetById(id).MapTo<AdminUserDto>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<AdminUserDto> List()
        {
            try
            {
                return rep.List(new Domain.Filters.BaseFilter()).EnumerableTo<AdminUserDto>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Update(AdminUserDto entity)
        {
            try
            {
                return rep.Update(entity.MapTo<AdminUser>());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public AdminUserDto SignIn(string email, string password)
        {
            AdminUserDto result = null;
            try
            {
                result = GetByEmail(email);
                if (result != null)
                {
                    if (!VerifyPassword(result.Password, password))
                        result = null;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        private bool VerifyPassword(string hash, string password)
        {
            return (hash == HashPass(password));
        }

        private string HashPass(string pass)
        {
            SHA256CryptoServiceProvider cryptoTransform = new SHA256CryptoServiceProvider();
            string hash = BitConverter.ToString(cryptoTransform.ComputeHash(Encoding.Default.GetBytes(pass))).Replace("-", "");
            return hash.ToLower();
        }
    }
}