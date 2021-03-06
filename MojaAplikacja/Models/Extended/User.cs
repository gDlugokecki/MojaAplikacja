﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MojaAplikacja.Models
{
    [MetadataType(typeof(UserMetaDate))]
    public partial class User
    {
        //[NotMapped]
        //public string ConfirmPassword { get; set; }

    }
    public class UserMetaDate
    {
        [Required]
        [DisplayName("User Name")]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        
        public string Password { get; set; }
        [Required]
        [DisplayName("Email Adress")]
        public string EmailAdress { get; set; }
        [Required]
        [Display(Name ="Date of Birth")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString ="{0:MM/dd/yyyy}")]
        public DateTime DateOfBirth { get; set; }

        public string PhotoPath { get; set; }
        
        public bool EmailConf { get; set; }
        public string ActivationCode { get; set; }
    }
}