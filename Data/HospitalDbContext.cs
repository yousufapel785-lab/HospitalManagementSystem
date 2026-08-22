using HospitalManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Data
{
    public class HospitalDbContext : DbContext
    {
        public HospitalDbContext(DbContextOptions<HospitalDbContext> options)
            : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Appointment> Appointments { get; set; }

        public DbSet<MedicalRecord> MedicalRecords { get; set; }

        public DbSet<Prescription> Prescriptions { get; set; }

        public DbSet<Treatment> Treatments { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Department → Doctors
            modelBuilder.Entity<Doctor>()
                .HasOne<Department>()
                .WithMany()
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Patient → Appointments
            modelBuilder.Entity<Appointment>()
     .HasOne(a => a.Patient)
     .WithMany(p => p.Appointments)
     .HasForeignKey(a => a.PatientId)
     .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Doctor)
                .WithMany(d => d.Appointments)
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Patient → Medical Records
            modelBuilder.Entity<MedicalRecord>()
                .HasOne<Patient>()
                .WithMany()
                .HasForeignKey(m => m.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            // Doctor → Medical Records
            modelBuilder.Entity<MedicalRecord>()
                .HasOne<Doctor>()
                .WithMany()
                .HasForeignKey(m => m.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Patient → Prescriptions
            modelBuilder.Entity<Prescription>()
                .HasOne<Patient>()
                .WithMany()
                .HasForeignKey(p => p.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            // Doctor → Prescriptions
            modelBuilder.Entity<Prescription>()
                .HasOne<Doctor>()
                .WithMany()
                .HasForeignKey(p => p.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Patient → Treatments
            modelBuilder.Entity<Treatment>()
                .HasOne<Patient>()
                .WithMany()
                .HasForeignKey(t => t.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            // Doctor → Treatments
            modelBuilder.Entity<Treatment>()
                .HasOne<Doctor>()
                .WithMany()
                .HasForeignKey(t => t.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Medical Record → Treatments
            modelBuilder.Entity<Treatment>()
                .HasOne<MedicalRecord>()
                .WithMany()
                .HasForeignKey(t => t.MedicalRecordId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}