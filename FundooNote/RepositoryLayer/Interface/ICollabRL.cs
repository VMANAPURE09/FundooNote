using RepositoryLayer.Services.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface ICollabRL
    {
        Task<Collaborator>AddCollab(int userId, int NoteId, string CollabEmail);
        Task<List<Collaborator>> GetAllCollab(int userId);
        Task DeleteCollab(int UserId, int NoteId);
    }
}
