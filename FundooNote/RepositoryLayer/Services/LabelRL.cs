using CommonLayer.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using RepositoryLayer.Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class LabelRL : ILabelRL
    {
        readonly FundooContext fundooContext;
        private IConfiguration _config;
        public LabelRL(FundooContext fundooContext, IConfiguration config)
        {
            this.fundooContext = fundooContext;
            this._config = config;
        }

        public async Task AddLabel(int UserId, int NoteId, string labelName)
        {
            try
            {
                Label label = new Label();

                label.UserId = UserId;
                label.NoteId = NoteId;
                label.LabelName = labelName;
                fundooContext.Labels.Add(label);
                await fundooContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Label> GetLabelByNoteId(int userId, int NoteId)
        {
            try
            {
                var user = fundooContext.Users.Where(x => x.UserId == userId).FirstOrDefault();
                var note = fundooContext.Notes.Where(x => x.NoteId == NoteId && x.UserId == userId).FirstOrDefault();
                var label = fundooContext.Labels.Where(x => x.NoteId == NoteId).FirstOrDefault();

                if (label == null)
                {
                    return null;
                }

                return await fundooContext.Labels.Where(x => x.NoteId == NoteId).FirstOrDefaultAsync();
            }



            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<GetLabelModel>> GetLabelByNoteIdwithJoin(int userId, int NoteId)
        {
            try
            {
                var label = await this.fundooContext.Labels.FirstOrDefaultAsync(x => x.UserId == userId);
                var result = await(from user in fundooContext.Users
                                   join notes in fundooContext.Notes on user.UserId equals userId //where notes.NoteId == NoteId
                                   join labels in fundooContext.Labels on notes.NoteId equals labels.NoteId
                                   where labels.NoteId == NoteId && labels.UserId == userId
                                   select new GetLabelModel
                                   {

                                       UserId = userId,
                                       NoteId = notes.NoteId,
                                       Title = notes.Title,
                                       FirstName = user.FirstName,
                                       LastName = user.LastName,
                                       Email = user.Email,
                                       Description = notes.Description,
                                       Color = notes.Color,
                                       LabelName = labels.LabelName,
                                       CreatedDate = labels.user.CreatedDate
                                   }).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<GetLabelModel>> GetLabelByUserIdWithJoin(int UserId)
        {
            try
            {
                var label = this.fundooContext.Labels.FirstOrDefaultAsync(x => x.UserId == UserId);
                var result = await(from user in fundooContext.Users
                                   join notes in fundooContext.Notes on user.UserId equals UserId //where notes.NoteId == NoteId
                                   join labels in fundooContext.Labels on notes.NoteId equals labels.NoteId
                                   where labels.UserId == UserId
                                   select new GetLabelModel
                                   {

                                       UserId = UserId,
                                       NoteId = notes.NoteId,
                                       Title = notes.Title,
                                       FirstName = user.FirstName,
                                       LastName = user.LastName,
                                       Email = user.Email,
                                       Description = notes.Description,
                                       Color = notes.Color,
                                       LabelName = labels.LabelName,
                                       CreatedDate = labels.user.CreatedDate
                                   }).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task UpdateLabel(int UserId, int NoteId, string newLabel)
        {
            try
            {

                var label = await this.fundooContext.Labels.Where(x => x.NoteId == NoteId && x.UserId == UserId).FirstOrDefaultAsync();
                if (label != null)
                {
                    label.LabelName = newLabel;
                }
                await fundooContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            };
        }
    }
}
