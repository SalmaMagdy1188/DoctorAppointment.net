using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace DoctorAppointment.Models
{
	public class Doctor
	{
		[DisplayName("Doctor Id")]
		public int DoctorId { get; set; }

		[DisplayName("Doctor Name")]
		[Required(ErrorMessage = "You have to provide avalid name.")]
		[MinLength(2, ErrorMessage = "Name Mustn't be less than 2 characters.")]
		[MaxLength(20, ErrorMessage = "Name Mustn't exceed 20 characters.")]
		public string FirstName { get; set; }
		[DisplayName("Last Name")]
		public string LastName { get; set; }
		[Required(ErrorMessage = "You to provide avalid Specialization")]
		[DisplayName("Specialization  ")]
		public string Specialization { get; set; }

		[Required]
		[DisplayName("PhoneNumber")]
		public string PhoneNumber { get; set; }

		[Required]
		public string Email { get; set; }

		[DisplayName("Image")]
		[ValidateNever]
		public string ImagePath { get; set; }

		[DisplayName("Is Active")]
		public bool IsActive { get; set; }

		[DisplayName("Available At")]
		public DateTime AvailableAt { get; set; }
		[ValidateNever]
		public List<Patient> Patients { get; set; }

		
	}
}
