using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RepositoryLayer.Services.Entities
{
    public class Note
    {
        [Key]
        public int NoteId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public bool isPin { get; set; }
        public bool isRemainder { get; set; }
        public bool isArchieve { get; set; }
        public bool isTrash { get; set; }
        public DateTime Remainder { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public User user { get; set; }
    }
}
