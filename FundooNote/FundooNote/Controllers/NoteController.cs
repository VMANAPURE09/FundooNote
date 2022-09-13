using BusinessLayer.Interface;
using CommonLayer.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RepositoryLayer.Migrations;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooNote.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class NoteController : Controller
    {
        INoteBL noteBL;

        private IConfiguration _config;
        private FundooContext fundooContext;
        private readonly IDistributedCache _cache;
        private readonly IMemoryCache _memoryCache;
        public NoteController(INoteBL noteBL, IConfiguration config, FundooContext fundooContext, IDistributedCache cache, IMemoryCache memoryCache)
        {
            this.noteBL = noteBL;
            this._config =config;
            this.fundooContext = fundooContext;
            this._cache = cache;
            this._memoryCache = memoryCache;

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

        [Authorize]
        [HttpPut("UpdateNote/{NoteId}")]
        public IActionResult UpdateNote(int NoteId, UpdateNoteModel updateNoteModel)
        {
            try
            {
                var note = fundooContext.Notes.Where(x => x.NoteId == NoteId).FirstOrDefault();
                //Authorization match userId
                var userId = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int UserID = Int32.Parse(userId.Value);
                if (note == null)
                {
                    return this.BadRequest(new { success = false, message = "Please provide correct note" });
                }
                this.noteBL.UpdateNote(UserID, NoteId, updateNoteModel);
                return this.Ok(new { success = true, status = 200, message = "Note Updated successfully" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpDelete("DeleteNote/{NoteId}")]
        public IActionResult DeleteNote(int NoteId)
        {
            try
            {
                var note = fundooContext.Notes.Where(x => x.NoteId == NoteId).FirstOrDefault();
                //Authorization match userId
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int UserID = Int32.Parse(userid.Value);
                if (note == null)
                {
                    return this.BadRequest(new { success = false, message = "Please provide correct note" });
                }
                this.noteBL.DeleteNote(UserID, NoteId);
                return this.Ok(new { success = true, status = 200, message = "Note Deleted successfully" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpGet("GetNote/{NoteId}")]
        public IActionResult GetNote(int NoteId)
        {
            try
            {
                var note = fundooContext.Notes.Where(x => x.NoteId == NoteId).FirstOrDefault();
                //Authorization match userId
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserID = Int32.Parse(userid.Value);
                if (note == null)
                {
                    return this.BadRequest(new { success = false, message = "Note not exist" });
                }
                //Note notes = new Note();
                 var notes = noteBL.GetNote(UserID, NoteId);
                return this.Ok(new { success = true, status = 200, note = notes });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpGet("GetAllNote")]
        public IActionResult GetAllNote()
        {
            try
            {

                //Authorization match userId
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserID = Int32.Parse(userid.Value);

                //List<Note> notes = new List<Note>();
                var note = noteBL.GetAllNotes(UserID);
                return this.Ok(new { success = true, status = 200, Allnotes = note });
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        [Authorize]
        [HttpGet("GetAllNoteByUsingJoin")]
        public IActionResult GetAllNotesByUsingJoin()
        {
            try
            {

                //Authorization match userId
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserID = Int32.Parse(userid.Value);

                List<NoteResponseModel> notes = new List<NoteResponseModel>();
                notes = this.noteBL.GetAllNotesByUsingJoin(UserID);
                return this.Ok(new { success = true, status = 200, Allnotes = notes });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpGet("ArchieveNote/{NoteId}")]
        public async Task<IActionResult> ArchieveNote(int NoteId)
        {
            try
            {
                var note = await fundooContext.Notes.Where(x => x.NoteId == NoteId).FirstOrDefaultAsync();
                if (note == null)
                {
                    return this.BadRequest(new { success = false, status = 400, message = "Note doesn't exist" });
                }
                //Authorization match userId
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserID = Int32.Parse(userid.Value);


                var archieve = await this.noteBL.ArchieveNote(UserID, NoteId);
                return this.Ok(new { success = true, status = 200, message = "Note archieved successfully" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpPut("PinNote/{NoteId}")]
        public async Task<IActionResult> PinNote(int NoteId)
        {
            try
            {
                var note = await fundooContext.Notes.Where(x => x.NoteId == NoteId).FirstOrDefaultAsync();
                if (note == null)
                {
                    return this.BadRequest(new { success = false, status = 400, message = "Note doesn't exist" });
                }
                //Authorization match userId
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int UserID = Int32.Parse(userid.Value);


                var pin = await this.noteBL.PinNote(UserID, NoteId);
                return this.Ok(new { success = true, status = 200, message = "Note Pinned successfully" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpPut("TrashNote/{NoteId}")]
        public async Task<IActionResult> TrashNote(int NoteId)
        {
            try
            {
                var note = await fundooContext.Notes.Where(x => x.NoteId == NoteId).FirstOrDefaultAsync();
                if (note == null)
                {
                    return this.BadRequest(new { success = false, status = 400, message = "Note doesn't exist" });
                }
                //Authorization match userId
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int UserID = Int32.Parse(userid.Value);

                bool trash = await this.noteBL.TrashNote(UserID, NoteId);
                return this.Ok(new { success = true, status = 200, message = "Note Trashed Successfully" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpPut("ReminderNote/{NoteId}")]
        public async Task<IActionResult> ReminderNote(int NoteId, NoteRemainderModel  reminder)
        {
            try
            {
                var note = await fundooContext.Notes.Where(x => x.NoteId == NoteId).FirstOrDefaultAsync();
                if (note == null)
                {
                    return this.BadRequest(new { success = false, status = 400, message = "Note doesn't exist" });
                }
                //Authorization match userId
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int UserID = Int32.Parse(userid.Value);
                var rem = Convert.ToDateTime(reminder.Reminder);
                await this.noteBL.ReminderNote(UserID, NoteId, rem);
                return this.Ok(new { success = true, status = 200, message = "Note  reminder added successfully" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpDelete("DeleteReminder/{NoteId}")]
        public async Task<IActionResult> DeleteReminderNote(int NoteId)
        {
            try
            {
                var note = await fundooContext.Notes.Where(x => x.NoteId == NoteId).FirstOrDefaultAsync();
                if (note == null)
                {
                    return this.BadRequest(new { success = false, status = 400, message = "Note doesn't exist" });
                }
                //Authorization match userId
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserID = Int32.Parse(userid.Value);
                await this.noteBL.DeleteReminder(UserID, NoteId);
                return this.Ok(new { success = true, status = 200, message = "Reminder Deleted successfully" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpPut("UpdateColor/{NoteId}/{color}")]
        public async Task<IActionResult> UpdateColor(int NoteId, string color)
        {
            try
            {
                var note = await fundooContext.Notes.Where(x => x.NoteId == NoteId).FirstOrDefaultAsync();
                if (note == null)
                {
                    return this.BadRequest(new { success = false, status = 400, message = "Note doesn't exist" });
                }
                //Authorization match userId
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int UserID = Int32.Parse(userid.Value);
                await this.noteBL.UpdateColor(UserID, NoteId, color);
                return this.Ok(new { success = true, status = 200, message = "Color updated successfully" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpGet("GetAllColor")]
        public IActionResult GetAllColor()
        {
            try
            {

                //Authorization match userId
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int UserID = Int32.Parse(userid.Value);

                List<GetColor> note = new List<GetColor>();
                note = this.noteBL.GetAllColor(UserID);
                return this.Ok(new { success = true, status = 200, noteList = note });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpGet("GetAllNoteByJoinUsingRedis")]
        public IActionResult GetAllNoteByUsingRedis()
        {
            try
            {
                string CacheKey = "NoteList";
                string serializeNoteList;
                var NoteList = new List<NoteResponseModel>();
                var RedisNoteList = _cache.Get(CacheKey);

                if (RedisNoteList != null)
                {
                    serializeNoteList = Encoding.UTF8.GetString(RedisNoteList);
                    NoteList = JsonConvert.DeserializeObject<List<NoteResponseModel>>(serializeNoteList);
                }
                else
                {
                    var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                    int UserID = Int32.Parse(userid.Value);
                    NoteList = this.noteBL.GetAllNotesByUsingJoin(UserID);
                    serializeNoteList = JsonConvert.SerializeObject(NoteList);
                    RedisNoteList = Encoding.UTF8.GetBytes(serializeNoteList);
                    var option = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(20)).SetAbsoluteExpiration(TimeSpan.FromHours(6));
                    _cache.Set(CacheKey, RedisNoteList, option);

                }
                return this.Ok(new { success = true, status = 200, noteList = NoteList });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

