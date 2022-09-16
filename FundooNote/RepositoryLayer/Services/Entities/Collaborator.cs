using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RepositoryLayer.Services.Entities
{
    public class Collaborator
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int collaboratorID { get; set; }
        public string CollabEmail { get; set; }

        public int? UserId { get; set; }
        public virtual User user { get; set; }

        public int? NoteId { get; set; }
        public virtual Note Note { get; set; }

       

    }
}
