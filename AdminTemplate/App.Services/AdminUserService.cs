using App.Domain.Entities;
using App.Domain.Repositories;
using App.Services.Dtos;
using App.Services.Interfaces;
using System;
using System.Collections.Generic;

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
    }
}