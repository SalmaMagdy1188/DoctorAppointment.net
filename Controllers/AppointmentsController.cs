using DoctorAppointment.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointment.Controllers
{
	public class AppointmentsController : Controller
	{
		ApplicationDbContext _context;
		
		public AppointmentsController(ApplicationDbContext context)
		{
			_context = context;
			
		}

		[HttpGet]
		
        public IActionResult GetIndexView(string? search)
        {
            ViewBag.Search = search;

            if (string.IsNullOrEmpty(search) == true)
            {
                return View("Index", _context.Patients.ToList());

            }
            else
            {
                return View("Index", _context.Patients.Where(d => d.FirstName.Contains(search) ||
                                                        d.LastName.Contains(search)).ToList());
            }

        }
    }
}
