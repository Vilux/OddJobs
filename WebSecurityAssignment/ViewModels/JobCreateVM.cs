﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebSecurityAssignment.ViewModels
{
    public class JobCreateVM
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public string EmployerID { get; set; }
        [Required]
        public float Amount { get; set; }
        [Required]
        [Display(Name = "Date Needed")]
        public DateTime dateNeeded { get; set; }
        [Required]
        [Display(Name = "Expiry Date")]
        public DateTime dateExpired { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Province { get; set; }
    }
}
