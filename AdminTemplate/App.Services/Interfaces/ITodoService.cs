﻿using System;
using System.Collections.Generic;
using System.Text;
using App.Services.Dtos;

namespace App.Services.Interfaces
{
    public interface ITodoService
    {
        TodoDto Create(TodoDto dto);

        IEnumerable<TodoDto> List(TodoFilterDto filter);

        TodoDto GetById(int id);

        bool Update(TodoDto todo);

        bool Delete(TodoDto dto);
    }
}
