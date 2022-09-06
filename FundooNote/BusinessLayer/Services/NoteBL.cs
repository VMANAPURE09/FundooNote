using BusinessLayer.Interface;
using CommonLayer.User;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
