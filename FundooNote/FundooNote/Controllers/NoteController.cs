﻿using BusinessLayer.Interface;
using CommonLayer.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Migrations;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
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
                Note notes = new Note();
                 this.noteBL.GetNote(UserID, NoteId);
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

                List<Note> notes = new List<Note>();
                this.noteBL.GetAllNotes(UserID);
                return this.Ok(new { success = true, status = 200, Allnotes = notes });
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

    }
}
