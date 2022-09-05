﻿using BusinessLayer.Interface;
using CommonLayer.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;

namespace FundooNote.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        IUserBL userBL;
        private IConfiguration _config;
        public UserController(IUserBL userBL, IConfiguration config)
        {
            this.userBL = userBL;
            this._config = config;
        }
        [HttpPost("RegisterUser")]
        public IActionResult RegisterUser(UserPostModel userPostModel)
        {
            try
            {
                this.userBL.RegisterUser(userPostModel);
                return this.Ok(new { sucess = true, status = 200, message = $"Registration sucessful for {userPostModel.Email}" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost("LoginUser")]
        public IActionResult LoginUser(LoginModel loginModel)
        {
            try
            {
                string token = this.userBL.LoginUser(loginModel);
                return this.Ok(new { success = true, status = 200, token = token, message = $"login successful for {loginModel.Email}" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost("ForgotPassword")]
        public IActionResult ForgotPassword(string email)
        {
            try
            {
                bool isExist = this.userBL.ForgotPassword(email);
                if (isExist) return Ok(new { success = true, message = $"Reset Link sent to Email : {email}" });
                else return BadRequest(new { success = false, message = $"No user Exist with Email : {email}" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
