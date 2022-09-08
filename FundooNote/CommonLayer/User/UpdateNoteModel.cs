using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.User
{
    public class UpdateNoteModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public bool isPin { get; set; }
        public bool isRemainder { get; set; }
        public bool isArchieve { get; set; }
        public bool isTrash { get; set; }
        public DateTime Remainder { get; set; }
    }
}
