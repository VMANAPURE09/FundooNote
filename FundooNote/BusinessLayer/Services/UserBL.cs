using BusinessLayer.Interface;
using CommonLayer.User;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class UserBL : IUserBL
    {
        readonly IUserRL userRL;
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }

        public bool ForgotPassword(string email)
        {
            try
            {
                return userRL.ForgotPassword(email);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string LoginUser(LoginModel loginModel)
        {
            try
            {
                return userRL.LoginUser(loginModel);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void RegisterUser(UserPostModel userPostModel)
        {
            try
            {
                this.userRL.RegisterUser(userPostModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ResetPassword(string email, PasswordModel passwordModel)
        {
            try
            {
                return this.userRL.ResetPassword(email, passwordModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
