using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RepositoryLayer.Services.Entities
{
    public class Label
    {
        public string LabelName { get; set; }
        public int NoteId { get; set; }
        public Note Note { get; set; }


        public int UserId { get; set; }
        public User user { get; set; }
    }
}
