﻿using App.Extensions;
using App.Results;
using App.Services.Dtos;
using App.Services.Interfaces;
using App.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace App.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class TodoController : Controller
    {

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            var isAuth = context.HttpContext.User.Identity.IsAuthenticated;
            var claims = context.HttpContext.User.Claims;
        }


        private readonly ITodoService appService;
        private readonly AppValidator validator;

        public TodoController(ITodoService appService, AppValidator validator)
        {
            this.appService = appService;
            this.validator = validator;
        }

        // GET: api/todo
        [HttpGet]
        public IActionResult Get([FromQuery]TodoFilterDto filter)
        {
            var result = new GenericResult<IEnumerable<TodoDto>>();
            try
            {
                result.Result = appService.List(filter);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Errors = new string[] { ex.Message };
            }
            return Ok(result);
        }

        // GET api/todo/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = new GenericResult<TodoDto>();

            try
            {
                result.Result = appService.GetById(id);
                if (result.Result == null)
                    throw new Exception($"Todo #{id} not found!");
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Errors = new string[] { ex.Message };
            }

            return Ok(result);
        }

        // POST api/todo
        [HttpPost]
        public IActionResult Post([FromBody]TodoDto model)
        {
            var result = new GenericResult<TodoDto>();
            var validatorResult = validator.Validate(model);

            if (validatorResult.IsValid)
            {
                try
                {
                    result.Result = appService.Create(model);
                    result.Success = true;
                }
                catch (Exception ex)
                {
                    result.Errors = new string[] { ex.Message };
                }
            }
            else
                result.Errors = validatorResult.GetErrors();

            return Ok(result);
        }

        // PUT api/todo/5
        [HttpPut]
        public IActionResult Put([FromBody]TodoDto model)
        {
            var result = new GenericResult();
            var validatorResult = validator.Validate(model);

            if (validatorResult.IsValid)
            {
                try
                {
                    result.Success = appService.Update(model);
                    if (!result.Success)
                        throw new Exception($"Todo #{model.Id} not found to be edited");
                }
                catch (Exception ex)
                {
                    result.Errors = new string[] { ex.Message };
                }
            }
            else
                result.Errors = validatorResult.GetErrors();

            return Ok(result);
        }

        // DELETE api/todo/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = new GenericResult();
            try
            {
                result.Success = appService.Delete(new TodoDto { Id = id });
                if (!result.Success)
                    throw new Exception($"Todo #{id} cant't be removed.");
            }
            catch (Exception ex)
            {
                result.Errors = new string[] { ex.Message };
            }
            return Ok(result);
        }
    }
}