using BusinessLayer.Interface;
using CommonLayer.User;
using RepositoryLayer.Interface;
using RepositoryLayer.Services.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class NoteBL : INoteBL
    {
        readonly INoteRL noteRL;
        public NoteBL(INoteRL noteRL)
        {
            this.noteRL = noteRL;
        }
        public void AddNote(int userId, NoteModel noteModel)
        {
            try
            {
                this.noteRL.AddNote(userId, noteModel);
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
                return await this.noteRL.ArchieveNote(userId, NoteId);

            }
            catch ( Exception ex)
            {
                throw ex;
            }
        }


        public bool DeleteNote(int userId, int NoteId)
        {
            try
            {
                return this.noteRL.DeleteNote(userId, NoteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteReminder(int userId, int NoteId)
        {
            try
            {
                return await noteRL.DeleteReminder(userId, NoteId);
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
                return noteRL.GetAllNotes(userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<NoteResponseModel> GetAllNotesByUsingJoin(int userId)
        {
            try
            {
                return noteRL.GetAllNotesUsingJoin(userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public Note GetNote(int userId, int NoteId)
        {
            try
            {
                return noteRL.GetNote(userId, NoteId);
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
                return await this.noteRL.PinNote(userId, NoteId);
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
                return await noteRL.ReminderNote(userId, NoteId, reminder);
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
                return await noteRL.TrashNote(userId, NoteId);
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
                await noteRL.UpdateColor(userId, NoteId, Color);
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
                this.noteRL.UpdateNote(userId, NoteId, updateNoteModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
