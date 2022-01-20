﻿using DevExtreme.AspNet.Data;
using ItServiceApp.Extensions;
using ItServiceApp.Models.Identity;
using ItServiceApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ItServiceApp.Areas.Admin.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UserApiController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserApiController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }



        [HttpGet]
        public IActionResult GetUsers(DataSourceLoadOptions loadOptions) 
        {
            var data = _userManager.Users;

            return Ok(DataSourceLoader.Load(data, loadOptions));  
           
        }

        [HttpPut("update-users")]
        public async Task<IActionResult> UpdateUsers(string key, string values)
        {
            var data = _userManager.Users.FirstOrDefault(x => x.Id == key);
            if (data == null)
                return StatusCode(StatusCodes.Status409Conflict, new JsonResponseViewModel()
                {
                    IsSuccess = false,
                    ErrorMessage = "Kullanıcı Bulunamadı"
                });
            JsonConvert.PopulateObject(values, data);
            if (!TryValidateModel(data))
                return BadRequest(ModelState.ToFullErrorString());

            var result = await _userManager.UpdateAsync(data);
            if (!result.Succeeded)
                return BadRequest(new JsonResponseViewModel()
                { 
                    IsSuccess = false,
                    ErrorMessage = "Kullanıcı Güncellenemedi"
                });
            return Ok(new JsonResponseViewModel());
           
            
        }

        [HttpGet]
        public IActionResult GetTest()
        {
            var users = new List<UserProfileViewModel>();
            for (int i = 0; i < 10000; i++)
            {
                users.Add(new()
                {
                    Email = "Deneme" + 1,
                    Surname = "soyad" + 1,
                    Name = "ad" + 1,

                });            
            }
            return Ok(new JsonResponseViewModel()
            {
                Data = users
            });
        }
        
    }
}
