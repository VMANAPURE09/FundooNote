using BusinessLayer.Interface;
using CommonLayer.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Services;
using System;
using System.Linq;

namespace FundooNote.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class NoteController : Controller
    {
        INoteBL noteBL;

        private IConfiguration _config;
        private FundooContext fundooContext;
        public NoteController(INoteBL noteBL, IConfiguration config, FundooContext fundooContext)
        {
            this.noteBL = noteBL;
            this._config = config;
            this.fundooContext = fundooContext;

        }
        [Authorize]
        [HttpPost("AddNote")]
        public IActionResult AddNote(NoteModel noteModel)
        {
            try
            {
                //Authorization, match userid from token
                var Userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int UserID = Int32.Parse(Userid.Value);
                this.noteBL.AddNote(UserID, noteModel);
                return this.Ok(new { success = true, status = 200, message = "Note Added Sucessfully" });
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
