using CommonLayer.User;
using RepositoryLayer.Services.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface INoteRL
    {
        void AddNote(int userId, NoteModel noteModel);
        public void UpdateNote(int userId, int NoteId, UpdateNoteModel updateNoteModel);
        public bool DeleteNote(int userId, int NoteId);
        public Note GetNote(int UserId, int NoteId);
        public List<Note> GetAllNotes(int userId);
        public List<NoteResponseModel> GetAllNotesUsingJoin(int UserId);

        Task<bool> ArchieveNote(int userId, int NoteId);
    }
}
