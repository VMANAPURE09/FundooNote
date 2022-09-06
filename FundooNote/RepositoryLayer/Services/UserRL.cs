using CommonLayer.User;
using Experimental.System.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interface;
using RepositoryLayer.Services.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {
        readonly FundooContext fundooContext;
        private IConfiguration _config;

        public UserRL(FundooContext fundooContext, IConfiguration config)
        {
            this.fundooContext = fundooContext;
            this._config = config;
        }

        public string LoginUser(LoginModel loginModel)
        {
            try
            {
                var user = fundooContext.Users.Where(x => x.Email == loginModel.Email && x.Password == loginModel.Password).FirstOrDefault();
                if (user == null)
                {
                    return null;
                }
                return GenerateJwtToken(user.Email, user.UserId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string GenerateJwtToken(string email, int userId)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim("Email", email),
                    new Claim("UserId",userId.ToString()),
                    }),
                    Expires = DateTime.UtcNow.AddHours(2),

                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature),
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void RegisterUser(UserPostModel userPostModel)
        {
            try
            {
                User user = new User();
                user.FirstName = userPostModel.FirstName;
                user.LastName = userPostModel.LastName;
                user.Email = userPostModel.Email;
                user.Password = userPostModel.Password;
                user.CreatedDate = DateTime.Now;
                user.ModifiedDate = DateTime.Now;
                fundooContext.Users.Add(user);
                fundooContext.SaveChanges();

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool ForgotPassword(string email)
        {
            try
            {
                var user = fundooContext.Users.Where(x => x.Email == email).FirstOrDefault();
                if (user == null)
                {
                    return false;
                }
                MessageQueue fundooQ = new MessageQueue();

                //Setting the QueuPath where we want to store the messages.
                fundooQ.Path = @".\private$\FundooNote";
                if (MessageQueue.Exists(fundooQ.Path))
                {

                    fundooQ = new MessageQueue(@".\private$\FundooNote");
                    //Exists
                }
                else
                {
                    // Creates the new queue named "Bills"
                    MessageQueue.Create(fundooQ.Path);
                }
                Message MyMessage = new Message();
                MyMessage.Formatter = new BinaryMessageFormatter();
                MyMessage.Body = GenerateJwtToken(email, user.UserId);
                MyMessage.Label = "Forget Password Email";
                fundooQ.Send(MyMessage);
                Message msg = fundooQ.Receive();
                msg.Formatter = new BinaryMessageFormatter();
                EmailService.SendEmail(email, msg.Body.ToString(), user.FirstName);
                fundooQ.ReceiveCompleted += new ReceiveCompletedEventHandler(msmqQueue_ReceiveCompleted);

                fundooQ.BeginReceive();
                fundooQ.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void msmqQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                MessageQueue queue = (MessageQueue)sender;
                Message msg = queue.EndReceive(e.AsyncResult);
                EmailService.SendEmail(e.Message.ToString(), GenerateToken(e.Message.ToString()), e.Message.ToString());
                queue.BeginReceive();
            }
            catch (MessageQueueException ex)

            {

                if (ex.MessageQueueErrorCode ==

                    MessageQueueErrorCode.AccessDenied)

                {

                    Console.WriteLine("Access is denied. " +

                        "Queue might be a system queue.");

                }

            }
        }

        private string GenerateToken(string email)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim("Email", email),

                    }),
                    Expires = DateTime.UtcNow.AddHours(2),

                    SigningCredentials =
                     new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature),
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
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
                var user = fundooContext.Users.Where(x => x.Email == email).FirstOrDefault();
                if (passwordModel.NewPassword != passwordModel.ConfirmNewPassword)
                {
                    return false;
                }
                user.Password = passwordModel.NewPassword;
                fundooContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
