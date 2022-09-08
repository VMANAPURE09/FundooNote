using CommonLayer.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface INoteRL
    {
        void AddNote(int userId, NoteModel noteModel);
        public void UpdateNote(int userId, int NoteId, UpdateNoteModel updateNoteModel);
    }
}
