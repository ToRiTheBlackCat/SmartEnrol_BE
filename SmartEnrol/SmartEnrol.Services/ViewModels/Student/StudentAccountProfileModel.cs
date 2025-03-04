using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using SmartEnrol.Repositories;
using SmartEnrol.Repositories.Models;
using SmartEnrol.Services;

namespace SmartEnrol.Services.ViewModels.Student
{
    public class StudentAccountProfileModel
    {
        [Required]
        public required string AccountId { get; set; }

        [Required]
        public required string AccountName { get; set; }

        [Required]
        public required string Email { get; set; }

        public int? AreaId { get; set; }
    }   

}
