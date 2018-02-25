using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ankarunning.Web.Models;
using Ankarunning.Data;
using Ankarunning.Service;
using System.Globalization;
using Ankarunning.Web.Helpers;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Ankarunning.Web.Controllers {
    public class HomeController : Controller {

        private readonly ITrainingService<Training> _trainingService;
        private readonly IPhotoService<TrainingPhoto> _trainingPhotoService;
        //constructor DI
        public HomeController(ITrainingService<Training> trainingService,
            IPhotoService<TrainingPhoto> trainingPhotoService) {
            _trainingService = trainingService;
            _trainingPhotoService = trainingPhotoService;
        }

        public IActionResult Index() {
            return View();
        }

        public IActionResult DownloadTrainingPhoto(long trainingId) {

            TrainingPhoto photo = _trainingPhotoService.GetPhotoWithForeignKey(trainingId);

            if (photo == null) {
                return View("Error", new ErrorViewModel { ErrorMessage = "Not Found!" });
            }

            return File(photo.Content, photo.ContentType, photo.Name);

        }

        #region Training
        public IActionResult Trainings() {
            //read all trainings so far and send to the page
            List<TrainingViewModel> trainings = _trainingService.GetAllTrainings()
                .Select(e => new TrainingViewModel {
                    Id = e.Id,
                    Title = e.Title,
                    Place = e.Place,
                    DateTime = e.DateTime,
                    Description = e.Description,
                    PhotoName = e.TrainingPhoto.Name
                }).OrderByDescending(e => e.Id).ToList();

            return View(trainings);
        }

        public IActionResult AddTraining() {
            return View();
        }

        public IActionResult EditTraining(long trainingId) {
            Training training = _trainingService.GetTrainingWithId(trainingId);
            TrainingViewModel twm = new TrainingViewModel{
              Id = training.Id,
              Title = training.Title,
              Place = training.Place,
              Description = training.Description,
              Date = training.DateTime,
              Time = training.DateTime,             
            };

            //date and time in turkish format has problems so put them in viewbag
            ViewBag.Date = Convert.ToDateTime(twm.Date).ToString("yyyy-MM-dd");
            //ViewBag.Time = twm.Time.ToString("t");

            return View(twm);
        }

        [HttpPost]
        public IActionResult AddTraining(TrainingViewModel model) {

            if (!ModelState.IsValid) {
                return View(model);
            }

            //add training
            Training training = new Training {
                Title = model.Title.ToCapital(),
                Place = model.Place.ToCapital(),
                DateTime = model.Date.Add(model.Time.TimeOfDay),
                Description = model.Description,
                AddedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow
            };

            long trainingId = _trainingService.InsertTraining(training);

            //add related image if any; else do not bother now. 
            //TODO: Later give an error
            if (model.Photo != null) {
                string fileName = model.Photo.FileName;
                string contentType = model.Photo.ContentType;

                Stream stream = model.Photo.OpenReadStream();
                BinaryReader reader = new BinaryReader(stream);
                byte[] content = reader.ReadBytes((int)model.Photo.Length);

                TrainingPhoto photo = new TrainingPhoto {
                    Name = model.Photo.FileName,
                    ContentType = model.Photo.ContentType,
                    Content = content,
                    AddedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow,
                    TrainingId = trainingId
                };

                _trainingPhotoService.InsertPhoto(photo);
            }

            _trainingService.SaveTrainingChanges();
            _trainingPhotoService.SavePhotoChanges();

            return RedirectToAction("Trainings");

        }

        [HttpPost]
        public IActionResult EditTraining(TrainingViewModel model) {

            if (!ModelState.IsValid) {
                return View(model);
            }

            //get training
            Training training = _trainingService.GetTrainingWithId(model.Id);

            if (training == null) {
                ModelState.AddModelError("Error", "No such data!");
                return View(model);
            }

            training.Title = model.Title.ToCapital();
            training.Place = model.Place.ToCapital();
            training.DateTime = model.Date.Add(model.Time.TimeOfDay);
            training.Description = model.Description;
            training.ModifiedDate = DateTime.UtcNow;

            _trainingService.UpdateTraining(training);

            //delete old add new photo if any provided; else do not bother. 
            if (model.Photo != null) {
                string fileName = model.Photo.FileName;
                string contentType = model.Photo.ContentType;

                Stream stream = model.Photo.OpenReadStream();
                BinaryReader reader = new BinaryReader(stream);
                byte[] content = reader.ReadBytes((int)model.Photo.Length);

                //delete existing training photo
                TrainingPhoto photo = _trainingPhotoService.GetPhotoWithForeignKey(model.Id);
                if(photo != null) {
                    _trainingPhotoService.DeletePhoto(photo);                
                }

                //add new
                photo = new TrainingPhoto {
                    Name = model.Photo.FileName,
                    ContentType = model.Photo.ContentType,
                    Content = content,
                    AddedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow,
                    TrainingId = model.Id
                };
                _trainingPhotoService.InsertPhoto(photo);
            }

            _trainingService.SaveTrainingChanges();
            _trainingPhotoService.SavePhotoChanges();

            return RedirectToAction("Trainings");

        }
        #endregion

        public IActionResult Events() {
            ViewData["Message"] = "Your contact page.";

            return View();
        }


        public IActionResult Profiles() {
            ViewData["Message"] = "Your Profiles page.";
            return View();
        }

        public IActionResult Photos() {
            ViewData["Message"] = "Your photos description page.";
            return View();
        }

        public IActionResult Club() {
            ViewData["Message"] = "Your club description page.";
            return View();
        }

        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
