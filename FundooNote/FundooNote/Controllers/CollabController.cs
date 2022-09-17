using BusinessLayer.Interface;
using CommonLayer.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Services;
using RepositoryLayer.Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNote.Controllers
{
    [ApiController]
    [Route("Api/[Controller]")]
    public class CollabController : Controller
    {
        FundooContext fundooContext;
        ICollabBL collabBL;
        public CollabController(FundooContext fundooContext, ICollabBL collabBL)
        {
            this.fundooContext = fundooContext;
            this.collabBL = collabBL;

        }

        [Authorize]
        [HttpPost("AddCollab/{NoteId}")]
        public async Task<ActionResult> AddCollab(int NoteId, CollabValidation collabValidation)
        {
            try
            {
                var currentUser = HttpContext.User;
                int Userid = Convert.ToInt32(currentUser.Claims.FirstOrDefault(c => c.Type == "UserId").Value);


                var collab = fundooContext.Collaborators.FirstOrDefault(c => c.UserId == Userid && c.NoteId == NoteId);
                if (collab != null)
                {
                    return BadRequest(new { success = false, Message = "Try With Some Other NoteId" });
                }
                await this.collabBL.AddCollab(Userid, NoteId, collabValidation.CollabEmail);
                return Ok(new { success = true, Message = "Collaborator Added Successfully" });
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult> GetAllCollaborator()
        {
            try
            {
                var currentUser = HttpContext.User;
                int Userid = Convert.ToInt32(currentUser.Claims.FirstOrDefault(c => c.Type == "UserId").Value);

                var collab = fundooContext.Collaborators.FirstOrDefault(c => c.UserId == Userid);
                if (collab == null)
                {
                    return BadRequest(new { success = false, Message = "Collaborator not Exists" });

                }
                List<Collaborator> collabList = new List<Collaborator>();
                collabList = await this.collabBL.GetAllCollab(Userid);
                return Ok(new { success = true, Message = "Collaborator Obtained Successfully", data = collabList });
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [Authorize]
        [HttpDelete("DeleteCollaborator/{Noteid}")]
        public async Task<ActionResult> DeleteCollaborator(int Noteid)
        {
            try
            {
                var currentUser = HttpContext.User;
                var UserId = Convert.ToInt32(currentUser.Claims.FirstOrDefault(c => c.Type == "UserId").Value);

                var collab = fundooContext.Collaborators.FirstOrDefault(c => c.UserId == UserId && c.NoteId == Noteid);
                if (collab == null)
                {
                    BadRequest(new { success = false, Message = "Collaborator Doesn't Exists" });

                }
                await this.collabBL.DeleteCollab(UserId, Noteid);
                return Ok(new { success = true, Message = "Collaborator Deleted Successfully" });
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
