using DoctorAppointment.Data;
using DoctorAppointment.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointment.Controllers
{
	public class PatientsController : Controller
	{
		ApplicationDbContext _context;
		IWebHostEnvironment _webHostEnvironment;
		public PatientsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
		{
			_context = context;
			_webHostEnvironment = webHostEnvironment;
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
														d.Address.Contains(search)).ToList());
			}

		}




		[HttpGet]
		public IActionResult GetDetailsView(int id)
		{

			Patient pat = _context.Patients.Include(d => d.Doctors).FirstOrDefault(d => d.PatientId == id);

			ViewBag.CurrentDept = pat;

			if (pat == null)
			{
				return NotFound();
			}
			else
			{
				return View("Details", pat);
			}

		}

		[HttpGet]
		public IActionResult GetCreateView()
		{
			ViewBag.AllDoctors = _context.Doctors.ToList();
			return View("Create");
		}

		[HttpPost]
		public IActionResult AddNew(Patient pat, IFormFile? imageFormFile)
		{
			//(GUID)->Globally Unique Identifier
			if (imageFormFile != null)
			{
				string imgExtension = Path.GetExtension(imageFormFile.FileName);
				Guid imgGuid = Guid.NewGuid();
				string imgName = imgGuid + imgExtension;
				string imgPath = "\\images\\" + imgName;
				pat.ImagePath = imgPath;
				string imgFullPath = _webHostEnvironment.WebRootPath + imgPath;
				FileStream imgFileStream = new FileStream(imgFullPath, FileMode.Create);
				imageFormFile.CopyTo(imgFileStream);
				imgFileStream.Dispose();
			}
			else
			{
				pat.ImagePath = "\\images\\No_Image.png";
			}

			if (ModelState.IsValid == true)
			{
				_context.Patients.Add(pat);
				_context.SaveChanges();
				return RedirectToAction("GetIndexView");
			}
			else
			{
                ViewBag.AllDoctors = _context.Doctors.ToList();
                return View("Create", pat); //(RedirectToAction)->Doesn't save data in case error

			}

		}

		[HttpGet]
		public IActionResult GetEditView(int id)
		{

			Patient patient= _context.Patients.FirstOrDefault(d => d.PatientId == id);
			if (patient == null)
			{
				return NotFound();
			}
			else
			{
				ViewBag.AllDoctors = _context.Doctors.ToList();
				return View("Edit", patient);
			}

		}



		[HttpPost]
		public IActionResult EditCurrent(Patient patient, IFormFile? imageFormFile)
		{
			if (ModelState.IsValid == true)
			{
				if (imageFormFile != null)
				{
					if (patient.ImagePath != "\\images\\No_Image.png")
					{
						string oldImgFullPath = _webHostEnvironment.WebRootPath + patient.ImagePath;
						System.IO.File.Delete(oldImgFullPath);
					}
					string imgExtension = Path.GetExtension(imageFormFile.FileName);
					Guid imgGuid = Guid.NewGuid();
					string imgName = imgGuid + imgExtension;
					string imgPath = "\\images\\" + imgName;
					patient.ImagePath = imgPath;
					string imgFullPath = _webHostEnvironment.WebRootPath + imgPath;
					FileStream imgFileStream = new FileStream(imgFullPath, FileMode.Create);
					imageFormFile.CopyTo(imgFileStream);
					imgFileStream.Dispose();
				}

				_context.Patients.Update(patient);
				_context.SaveChanges();
				return RedirectToAction("GetIndexView");
			}
			else
			{
				ViewBag.AllDoctors = _context.Doctors.ToList();
				return View("Edit", patient); //(RedirectToAction)->Doesn't save data in case error

			}

		}

		[HttpGet]
		public IActionResult GetDeleteView(int id)
		{

			Patient patient = _context.Patients.Include(d => d.Doctors).FirstOrDefault(d => d.PatientId == id);
			ViewBag.CurrentDept = patient;
			if (patient == null)
			{
				return NotFound();
			}
			else
			{
				return View("Delete", patient);
			}
		}


		[HttpPost]
		public IActionResult DeleteCurrent(int PatientId)
		{
			Patient patient = _context.Patients.Find(PatientId);
			if (patient != null && patient.ImagePath != "\\images\\No_Image.png")
			{
				string imgFullPath = _webHostEnvironment.WebRootPath + patient.ImagePath;
				System.IO.File.Delete(imgFullPath);
			}
			_context.Patients.Remove(patient);
			_context.SaveChanges();

			return RedirectToAction("GetIndexView");
		}
	}
}
