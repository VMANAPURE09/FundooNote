using Microsoft.Extensions.Configuration;
using BusinessLayer.Interface;
using RepositoryLayer.Interface;
using RepositoryLayer.Services.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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
    }
}
