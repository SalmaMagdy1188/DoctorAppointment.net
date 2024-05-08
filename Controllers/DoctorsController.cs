using DoctorAppointment.Data;
using DoctorAppointment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointment.Controllers
{
	public class DoctorsController : Controller
	{
		ApplicationDbContext _context;
		IWebHostEnvironment _webHostEnvironment;
		public DoctorsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
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
				return View("Index", _context.Doctors.ToList());

			}
			else
			{
				return View("Index", _context.Doctors.Where(d => d.FirstName.Contains(search) ||
														d.Specialization.Contains(search)).ToList());
			}

		}




		[HttpGet]
		public IActionResult GetDetailsView(int id)
		{

			Doctor doc = _context.Doctors.Include(d => d.Patients).FirstOrDefault(d => d.DoctorId == id);

			ViewBag.CurrentDept = doc;

			if (doc == null)
			{
				return NotFound();
			}
			else
			{
				return View("Details", doc);
			}

		}

		[HttpGet]
		public IActionResult GetCreateView()
		{
			ViewBag.AllDoctors = _context.Patients.ToList();
			return View("Create");
		}

		[HttpPost]
		public IActionResult AddNew(Doctor doc, IFormFile? imageFormFile)
		{
			//(GUID)->Globally Unique Identifier
			if (imageFormFile != null)
			{
				string imgExtension = Path.GetExtension(imageFormFile.FileName);
				Guid imgGuid = Guid.NewGuid();
				string imgName = imgGuid + imgExtension;
				string imgPath = "\\images\\" + imgName;
				doc.ImagePath = imgPath;
				string imgFullPath = _webHostEnvironment.WebRootPath + imgPath;
				FileStream imgFileStream = new FileStream(imgFullPath, FileMode.Create);
				imageFormFile.CopyTo(imgFileStream);
				imgFileStream.Dispose();
			}
			else
			{
				doc.ImagePath = "\\images\\No_Image.png";
			}

			if (ModelState.IsValid == true)
			{
				_context.Doctors.Add(doc);
				_context.SaveChanges();
				return RedirectToAction("GetIndexView");
			}
			else
			{
				return View("Create", doc); //(RedirectToAction)->Doesn't save data in case error

			}

		}

		[HttpGet]

		public IActionResult GetEditView(int id)
		{

			Doctor doc = _context.Doctors.FirstOrDefault(d => d.DoctorId == id);
			if (doc == null)
			{
				return NotFound();
			}
			else
			{
				return View("Edit", doc);
			}

		}


		[HttpPost]
		public IActionResult EditCurrent(Doctor doctor, IFormFile? imageFormFile)
		{
			if (ModelState.IsValid == true)
			{
				if (imageFormFile != null)
				{
					if (doctor.ImagePath != "\\images\\No_Image.png")
					{
						string oldImgFullPath = _webHostEnvironment.WebRootPath + doctor.ImagePath;
						System.IO.File.Delete(oldImgFullPath);
					}
					string imgExtension = Path.GetExtension(imageFormFile.FileName);
					Guid imgGuid = Guid.NewGuid();
					string imgName = imgGuid + imgExtension;
					string imgPath = "\\images\\" + imgName;
					doctor.ImagePath = imgPath;
					string imgFullPath = _webHostEnvironment.WebRootPath + imgPath;
					FileStream imgFileStream = new FileStream(imgFullPath, FileMode.Create);
					imageFormFile.CopyTo(imgFileStream);
					imgFileStream.Dispose();
				}

				_context.Doctors.Update(doctor);
				_context.SaveChanges();
				return RedirectToAction("GetIndexView");
			}
			else
			{
				return View("Edit", doctor); //(RedirectToAction)->Doesn't save data in case error

			}

		}

		[HttpGet]
		public IActionResult GetDeleteView(int id)
		{
			
			Doctor doc = _context.Doctors.Include(d => d.Patients).FirstOrDefault(d => d.DoctorId == id);
			ViewBag.CurrentDept = doc;
			if (doc == null)
			{
				return NotFound();
			}
			else
			{
				return View("Delete", doc);
			}
		}


		[HttpPost]
		public IActionResult DeleteCurrent(int DoctorId)
		{
			Doctor doc = _context.Doctors.Find(DoctorId);
			if (doc != null && doc.ImagePath != "\\images\\No_Image.png")
			{
				string imgFullPath = _webHostEnvironment.WebRootPath + doc.ImagePath;
				System.IO.File.Delete(imgFullPath);
			}
			_context.Doctors.Remove(doc);
			_context.SaveChanges();

			return RedirectToAction("GetIndexView");
		}
	}
}
