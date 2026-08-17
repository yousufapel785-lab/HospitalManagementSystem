using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem.Models
{
    public class Treatment
    {
        [Key]
        public int TreatmentId { get; set; }

        [Required]
        public int PatientId { get; set; }

        [Required]
        public int DoctorId { get; set; }

        [Required]
        public int MedicalRecordId { get; set; }

        [Required]
        [StringLength(1000)]
        public string TreatmentDetails { get; set; } = string.Empty;

        [StringLength(500)]
        public string Medication { get; set; } = string.Empty;

        [StringLength(500)]
        public string Instructions { get; set; } = string.Empty;

        public DateTime TreatmentDate { get; set; } = DateTime.Now;

        [StringLength(100)]
        public string Status { get; set; } = "Ongoing";
    }
}