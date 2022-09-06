using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.User
{
    public class NoteModel
    {
        [Required]
        [RegularExpression("^[A-Z]{1}[a-zA-Z]{2,}$", ErrorMessage = "First letter start with cap letter")]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Color { get; set; }
    }
}
