using App.Domain.Entities;
using App.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Services.Interfaces
{
    public interface IAdminUserService: ICRUDService<AdminUserDto>
    {
        AdminUserDto GetByEmail(string email);

        AdminUserDto SignIn(string email, string password);


    }

    public interface ICRUDService<T>
    {
        T Create(T entity);

        IEnumerable<T> List();

        T GetById(int id);

        bool Update(T entity);

        bool Delete(T entity);
    }
}
