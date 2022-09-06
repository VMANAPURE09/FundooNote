using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.User
{
    public class PasswordModel
    {
        [Required]
        [RegularExpression("^(?=.*[A-Z])(?=.*[@#$!%^&-+=()])(?=.*[0-9])(?=.*[a-z]).{8,}$", ErrorMessage = "Password should have min 8 characters with atleast 1 UpperCase,1 Numeric Number & 1 SpecialCharacter ")]
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
    }
}
