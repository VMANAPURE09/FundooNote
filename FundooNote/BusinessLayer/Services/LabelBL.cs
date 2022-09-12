using Microsoft.Extensions.Configuration;
using BusinessLayer.Interface;
using RepositoryLayer.Interface;
using RepositoryLayer.Services.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CommonLayer.User;

namespace BusinessLayer.Services
{
    public class LabelBL: ILabelBL
    {
        readonly ILabelRL labelRL;
        public LabelBL(ILabelRL labelRL)
        {
            this.labelRL = labelRL;
        }

        public async Task AddLabel(int UserId, int NoteId, string labelName)
        {
            try
            {
                await this.labelRL.AddLabel(UserId, NoteId, labelName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteLabel(int UserId, int NoteId)
        {
            try
            {
                return await this.labelRL.DeleteLabel(UserId, NoteId);
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
                return await this.labelRL.GetLabelByNoteId(userId, NoteId);
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
                return await this.labelRL.GetLabelByNoteIdwithJoin(userId, NoteId);
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
                return await this.labelRL.GetLabelByUserIdWithJoin(UserId);
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
                 await this.labelRL.UpdateLabel(UserId, NoteId, newLabel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
