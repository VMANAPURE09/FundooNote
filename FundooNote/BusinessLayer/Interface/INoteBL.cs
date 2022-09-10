using CommonLayer.User;
using RepositoryLayer.Services.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface INoteBL
    {
        void AddNote(int userId, NoteModel noteModel);
        public void UpdateNote(int userId, int NoteId, UpdateNoteModel updateNoteModel);
        public bool DeleteNote(int userId, int NoteId);
        public Note GetNote(int userId, int NoteId);
        public List<Note> GetAllNotes(int userId);
        public List<NoteResponseModel> GetAllNotesByUsingJoin(int UserId);

        Task<bool> ArchieveNote(int userId, int NoteId);
        Task<bool> PinNote(int userId, int NoteId);
        Task<bool> TrashNote(int userId, int NoteId);
        Task<bool> ReminderNote(int userId, int NoteId, DateTime reminder);
        Task<bool> DeleteReminder(int userId, int NoteId);
        Task UpdateColor(int userId, int NoteId, string Color);
        public List<GetColor> GetAllColor(int userId);
    }
}
