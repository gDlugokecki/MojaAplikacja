﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MojaAplikacja.Models
{
    [MetadataType(typeof(UserMetaDate))]
    public partial class User
    {
        public string ConfirmPassword { get; set; }
    }
    public class UserMetaDate
    {
        [Required]
        [DisplayName("User Name")]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(20,MinimumLength =8)]
        
        public string Password { get; set; }
        [Required]
        [DisplayName("Confirm Password")]
        [Compare("Password",ErrorMessage ="Not match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        [Required]
        [DisplayName("Email Adress")]
        public string EmailAdress { get; set; }

        [Display(Name ="Date of Birth")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString ="{0:MM/dd/yyyy}")]
        public DateTime DateOfBirth { get; set; }
    }
}