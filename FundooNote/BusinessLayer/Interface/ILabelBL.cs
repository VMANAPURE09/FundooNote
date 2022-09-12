using CommonLayer.User;
using RepositoryLayer.Services.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface ILabelBL
    {
        Task AddLabel(int userId, int NoteId, string labelName);
        Task<Label> GetLabelByNoteId(int userId, int NoteId);
        Task<List<GetLabelModel>> GetLabelByNoteIdwithJoin(int userId, int NoteId);
        Task<List<GetLabelModel>> GetLabelByUserIdWithJoin(int UserId);
        Task UpdateLabel(int UserId, int NoteId, string newLabel);
        Task<bool> DeleteLabel(int UserId, int NoteId);
    }
}
