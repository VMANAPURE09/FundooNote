using BusinessLayer.Interface;
using RepositoryLayer.Interface;
using RepositoryLayer.Services.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class CollabBL: ICollabBL
    {

        ICollabRL collabRL;
        public CollabBL(ICollabRL collabRL)
        {
            this.collabRL = collabRL;
        }

        public async Task AddCollab(int userId, int NoteId, string CollabEmail)
        {
            try
            {
                await collabRL.AddCollab(userId, NoteId, CollabEmail);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task DeleteCollab(int UserId, int NoteId)
        {
            try
            {
                await this.collabRL.DeleteCollab(UserId, NoteId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<Collaborator>> GetAllCollab(int userId)
        {
            try
            {
                return await this.collabRL.GetAllCollab(userId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        
    }
}
