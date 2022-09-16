using RepositoryLayer.Services.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface ICollabBL
    {
        Task AddCollab(int userId, int NoteId, string CollabEmail);
        Task<List<Collaborator>> GetAllCollab(int userId);
        Task DeleteCollab(int UserId,int NoteId);
    }
}
