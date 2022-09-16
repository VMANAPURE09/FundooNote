using CommonLayer.User;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using RepositoryLayer.Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace RepositoryLayer.Services
{
    public class CollabRL : ICollabRL
    {
        FundooContext fundooContext;
        IConfiguration configuration;

        public CollabRL(FundooContext fundooContext, IConfiguration configuration)
        {
            this.fundooContext = fundooContext;
            this.configuration = configuration;
        }
        public async Task<Collaborator> AddCollab(int userId, int NoteId, string CollabEmail)
        {
            try
            {
                var user = fundooContext.Collaborators.FirstOrDefault(x => x.UserId == userId && x.NoteId == NoteId);
                Collaborator collaborator = new Collaborator();
                collaborator.UserId = userId;
                collaborator.NoteId = NoteId;
                collaborator.CollabEmail = CollabEmail;
                fundooContext.Collaborators.Add(collaborator);
                await fundooContext.SaveChangesAsync();
                return collaborator;
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteCollab(int UserId, int NoteId)
        {
            try
            {
                var collab = fundooContext.Collaborators.FirstOrDefault(c => c.UserId == UserId && c.NoteId == NoteId);
                if (collab != null)
                {
                    fundooContext.Collaborators.Remove(collab);
                    await fundooContext.SaveChangesAsync();
                }

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
                var collab = fundooContext.Collaborators.FirstOrDefault(c => c.UserId == userId);
                if (collab == null)
                {
                    return null;
                }
                return await fundooContext.Collaborators.ToListAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
