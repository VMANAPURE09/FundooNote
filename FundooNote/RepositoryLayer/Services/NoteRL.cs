using CommonLayer.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using RepositoryLayer.Interface;
using RepositoryLayer.Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Note = RepositoryLayer.Services.Entities.Note;

namespace RepositoryLayer.Services
{
    public class NoteRL : INoteRL
    {
        readonly FundooContext fundooContext;
        public NoteRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }
        public void AddNote(int userId, NoteModel noteModel)
        {
            try
            {
                Note note = new Note();
                note.UserId = userId;
                note.Title = noteModel.Title;
                note.Description = noteModel.Description;
                note.Color = noteModel.Color;
                note.Remainder = DateTime.Now;
                note.CreatedDate = DateTime.Now;
                note.ModifiedDate = DateTime.Now;

                fundooContext.Notes.Add(note);
                fundooContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> ArchieveNote(int userId, int NoteId)
        {
            try
            {
                var note = await fundooContext.Notes.Where(x => x.NoteId == NoteId).FirstOrDefaultAsync();
                if (note == null || note.isTrash == true)
                {
                    return false;
                }

                if (note.isArchieve == true)
                {
                    note.isArchieve = false;
                }
                note.isArchieve = true;
                await fundooContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteNote(int userId, int NoteId)
        {
            try
            {
                var note = fundooContext.Notes.Where(x => x.NoteId == NoteId).FirstOrDefault();
                if (note == null)
                {
                    return false;
                }
                fundooContext.Notes.Remove(note);
                fundooContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteReminder(int UserId, int NoteId)
        {
            try
            {
                var note = await fundooContext.Notes.Where(x => x.NoteId == NoteId).FirstOrDefaultAsync();
                if (note == null || note.isTrash == true)
                {
                    return false;
                }
                note.isRemainder = false;
                note.Remainder = DateTime.Now;

                fundooContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GetColor> GetAllColor(int userId)
        {
            try
            {
                return fundooContext.Users.Where(u => u.UserId == userId)
                .Join(fundooContext.Notes,
                u => u.UserId,
                n => n.UserId,
                (u, n) => new GetColor
                {
                    Color = n.Color
                }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Note> GetAllNotes(int userId)
        {
            try
            {
                var note = fundooContext.Notes.Where(x => x.UserId == userId).ToList(); //using LINQ
                return note;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<NoteResponseModel> GetAllNotesUsingJoin(int UserId)
        {
            try
            {
                //Using LINQ join
                return fundooContext.Users.Where(u => u.UserId == UserId)
                .Join(fundooContext.Notes,
                u => u.UserId,
                n => n.UserId,
                (u, n) => new NoteResponseModel
                {
                    NoteId = n.NoteId,
                    UserId = u.UserId,
                    Title = n.Title,
                    Description = n.Description,
                    Color = n.Color,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email

                }).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Note GetNote(int UserId, int NoteId)
        {
            try
            {
                var note = fundooContext.Notes.Where(x => x.NoteId == NoteId).FirstOrDefault();

                return note;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> PinNote(int userId, int NoteId)
        {
            try
            {

                var note = await fundooContext.Notes.Where(x => x.NoteId == NoteId).FirstOrDefaultAsync();
                if (note == null || note.isTrash == true)
                {
                    return false;
                }

                if (note.isPin == true)
                {
                    note.isPin = false;
                }
                else { note.isPin = true; }
                await fundooContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> ReminderNote(int userId, int NoteId, DateTime reminder)
        {
            try 
            {
                var note = await fundooContext.Notes.Where(x => x.NoteId == NoteId).FirstOrDefaultAsync();
                if (note == null || note.isTrash == true)
                {
                    return false;
                }
                note.isRemainder = true;
                note.Remainder = reminder;

                fundooContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> TrashNote(int userId, int NoteId)
        {
            try
            {
                var note = await fundooContext.Notes.Where(x => x.NoteId == NoteId).FirstOrDefaultAsync();
                if (note == null)
                {
                    return false;
                }
                if (note.isTrash == true)
                {
                    note.isTrash = false;
                }
                else { note.isTrash = true; }
                await fundooContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task UpdateColor(int userId, int NoteId, string Color)
        {
            try
            {
                var note = await fundooContext.Notes.Where(x => x.NoteId == NoteId).FirstOrDefaultAsync();
                if (note != null || note.isTrash != true)
                {
                    note.Color = Color;
                }
                fundooContext.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateNote(int userId, int NoteId, UpdateNoteModel updateNoteModel)
        {
            try
            {
                var note = fundooContext.Notes.Where(x => x.NoteId == NoteId).FirstOrDefault();

                note.Title = updateNoteModel.Title != "string" ? updateNoteModel.Title : note.Title;
                note.Description = updateNoteModel.Description != "string" ? updateNoteModel.Description : note.Description;
                note.Color = updateNoteModel.Color != "string" ? updateNoteModel.Color : note.Color;
                note.isPin = updateNoteModel.isPin;
                note.isRemainder = updateNoteModel.isRemainder;
                note.isArchieve = updateNoteModel.isArchieve;
                note.isTrash = updateNoteModel.isArchieve;
                note.Remainder = updateNoteModel.Remainder;
                note.ModifiedDate = DateTime.Now;
                fundooContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    
}
