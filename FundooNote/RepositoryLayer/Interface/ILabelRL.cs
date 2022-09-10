using CommonLayer.User;
using RepositoryLayer.Services.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface ILabelRL
    {
        Task AddLabel(int UserId, int NoteId, string labelName);
        Task<Label> GetLabelByNoteId(int userId, int NoteId);
        Task<List<GetLabelModel>> GetLabelByNoteIdwithJoin(int userId, int NoteId);
        Task<List<GetLabelModel>> GetLabelByUserIdWithJoin(int UserId);
    }
}
