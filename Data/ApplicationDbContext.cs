using DoctorAppointment.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace DoctorAppointment.Data
{
	public class ApplicationDbContext :DbContext
	{
		
		public DbSet<Doctor> Doctors { get; set; }
		public DbSet<Patient> Patients { get; set; }
		
		public ApplicationDbContext(DbContextOptions options) : base(options)
		{

		}
	}
}
