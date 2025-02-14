using System;
using System.Collections.Generic;
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
        public int AccountId { get; set; }

        public string AccountName { get; set; }

        public string Email { get; set; }
    }   

}
