using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace DoctorAppointment.Models
{
	public class Patient
	{
		

		public int PatientId { get; set; }
		[DisplayName("PatientName ")]
		[Required(ErrorMessage = "You have to provide avalid name.")]
		[MinLength(2, ErrorMessage = "Name Mustn't be less than 2 characters.")]
		[MaxLength(20, ErrorMessage = "Name Mustn't exceed 20 characters.")]
		public string FirstName { get; set; }

		[DisplayName("LastName ")]
		public string LastName { get; set; }

		[DisplayName("BirhtDate ")]
		public DateTime DateOfBirth { get; set; }

		public string Address { get; set; }

		[DisplayName("PhoneNumber ")]
		public string PhoneNumber { get; set; }

		public string Email { get; set; }

		public DateTime CreatedAt { get; set; }

		public DateTime UpdatedAt { get; set; }

		[DisplayName("Image")]
		[ValidateNever]
		public string ImagePath { get; set; }

		//forigen key
		[Range(1, double.MaxValue, ErrorMessage = "Select avalid doctor.")]
		[DisplayName("Doctor")]
		public int DoctorId { get; set; }
		[DisplayName("DoctorName ")]
		[ValidateNever]
		public Doctor Doctors { get; set; }

	
    }
}
