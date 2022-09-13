using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNote.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LabelController : Controller
    {

        ILabelBL labelBL;

        private IConfiguration _config;
        private FundooContext fundooContext;
        public LabelController(ILabelBL labelBL, IConfiguration config, FundooContext fundooContext)
        {
            this.labelBL = labelBL;
            this._config = config;
            this.fundooContext = fundooContext;
        }
        [Authorize]
        [HttpPost("AddLabel/{NoteId}/{labelName}")]
        public async Task<IActionResult>AddLabel(int NoteId, string labelName)
        {
            var labelNote = fundooContext.Notes.Where(x => x.NoteId == NoteId).FirstOrDefault();
            if (labelNote == null)
            {
                return this.BadRequest(new { success = false, status = 400, message = "Note doesn't exist" });
            }
            var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
            int UserID = Int32.Parse(userid.Value);

            await this.labelBL.AddLabel(UserID, NoteId, labelName);
            return this.Ok(new { success = true, status = 200, message = "Label added successfully" });
        }
        [Authorize]
        [HttpGet("GetLabels/{NoteId}")]
        public async Task<IActionResult> GetLabels(int NoteId)
        {
            var labelNote = fundooContext.Labels.Where(x => x.NoteId == NoteId).FirstOrDefault();
            if (labelNote == null)
            {
                return this.BadRequest(new { success = false, status = 400, message = "Note doesn't exist" });
            }
            var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
            int UserID = Int32.Parse(userid.Value);


            var labels = await this.labelBL.GetLabelByNoteId(UserID, NoteId);
            return this.Ok(new { success = true, status = 200, Labels = labels });
        }
        [Authorize]
        [HttpGet("GetLabelByNoteIdwithJoin/{NoteId}")]
        public async Task<IActionResult> GetLabelByNoteIdwithJoin(int NoteId)
        {
            var labelNote = fundooContext.Labels.Where(x => x.NoteId == NoteId).FirstOrDefault();
            if (labelNote == null)
            {
                return this.BadRequest(new { success = false, status = 400, message = "Note doesn't exist " });
            }
            var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
            int UserID = Int32.Parse(userid.Value);


            var labels = await this.labelBL.GetLabelByNoteIdwithJoin(UserID, NoteId);
            return this.Ok(new { success = true, status = 200, Labels = labels });
        }
        [Authorize]
        [HttpGet("GetLabelByUserIdWithJoin")]
        public async Task<IActionResult> GetLabelByUserIdWithJoin()
        {
            var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
            int UserID = Int32.Parse(userid.Value);


            var labels = await this.labelBL.GetLabelByUserIdWithJoin(UserID);
            return this.Ok(new { success = true, status = 200, Labels = labels });
        }

        [Authorize]
        [HttpPut("UpdateLabel/{NoteId}/{newLabel}")]
        public async Task<IActionResult> UpdateLabel(int NoteId, string newLabel)
        {
            var labelNote = await fundooContext.Labels.Where(x => x.NoteId == NoteId).FirstOrDefaultAsync();
            if (labelNote == null)
            {
                return this.BadRequest(new { success = false, status = 400, message = "Note doesn't exist " });
            }
            var Userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
            int UserID = Int32.Parse(Userid.Value);

            await this.labelBL.UpdateLabel(UserID, NoteId, newLabel);
            return this.Ok(new { success = true, status = 200, message = "Label Updated successfully" });
        }

        [Authorize]
        [HttpDelete("DeleteLabel/{NoteId}")]
        public async Task<IActionResult> DeleteLabel(int NoteId)
        {
            var labelNote = await fundooContext.Labels.Where(x => x.NoteId == NoteId).FirstOrDefaultAsync();
            if (labelNote == null)
            {
                return this.BadRequest(new { success = false, status = 400, message = "There is no Label created for this NoteId " });
            }
            var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
            int UserID = Int32.Parse(userid.Value);

            await this.labelBL.DeleteLabel(UserID, NoteId);
            return this.Ok(new { success = true, status = 200, message = "Label Deleted successfully" });
        }
    }
}
