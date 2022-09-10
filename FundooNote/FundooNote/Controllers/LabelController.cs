using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> AddLabel(int NoteId, string labelName)
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
    }
}
