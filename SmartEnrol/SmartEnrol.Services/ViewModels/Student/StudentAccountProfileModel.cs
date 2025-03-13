using SmartEnrol.Repositories.Models;
using System.ComponentModel.DataAnnotations;

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

        public string? AreaName { get; set; }

    }

}
