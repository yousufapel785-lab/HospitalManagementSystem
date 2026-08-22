using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem.Models
{
    public class Doctor
    {
        [Key]
        public int DoctorId { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        public string Specialization { get; set; } = string.Empty;

        public string Qualification { get; set; } = string.Empty;

        [Phone]
        public string Phone { get; set; } = string.Empty;

        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public string AvailableDays { get; set; } = string.Empty;

        public string AvailableTime { get; set; } = string.Empty;

        public int DepartmentId { get; set; }
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}