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

namespace Ankarunning.Web.Controllers
{
   public class HomeController : Controller
   {

      private readonly ITrainingService<Training> _trainingService;
      private readonly IPhotoService<TrainingPhoto> _trainingPhotoService;
      private readonly IEventService<Event> _eventService;
      private readonly IPhotoService<EventPhoto> _eventPhotoService;
      private readonly IRouteService<Route> _routeService;
      //constructor DI
      public HomeController(ITrainingService<Training> trainingService,
          IPhotoService<TrainingPhoto> trainingPhotoService,
          IEventService<Event> eventService,
          IPhotoService<EventPhoto> eventPhotoService,
          IRouteService<Route> routeService)
      {
         _trainingService = trainingService;
         _trainingPhotoService = trainingPhotoService;
         _eventService = eventService;
         _eventPhotoService = eventPhotoService;
         _routeService = routeService;
      }

      public IActionResult Index()
      {
         return View();
      }


      #region Training
      public IActionResult Trainings()
      {
         //read all trainings so far and send to the page
         List<TrainingViewModel> trainings = _trainingService.GetAllTrainings()
             .Select(e => new TrainingViewModel
             {
                Id = e.Id,
                Title = e.Title,
                DateTime = e.DateTime,
                Description = e.Description,
                AvgPace = e.AvgPace,
                Route = e.Route.Name,
                PhotoName = e.TrainingPhoto.Name
             }).OrderByDescending(e => e.DateTime).ToList();

         return View(trainings);
      }

      public IActionResult AddTraining()
      {
         //load Routes
         ViewBag.Routes = _routeService.GetAllRoutes().ToList();
         return View();
      }

      public IActionResult EditTraining(long trainingId)
      {
         TrainingViewModel twm = _trainingService.GetTrainingWithId(trainingId)
            .Select(t => new TrainingViewModel
            {
               Id = t.Id,
               Title = t.Title,
               RouteId = t.Route.Id,
               Description = t.Description,
               AvgPace = t.AvgPace,
               Date = t.DateTime,
               Time = t.DateTime,
            }).FirstOrDefault();

         //date and time in turkish format has problems so put them in viewbag
         ViewBag.Date = twm.Date.ToString("yyyy-MM-dd");
         ViewBag.Time = twm.Time.ToString("HH:mm:ss");
         //load Routes
         ViewBag.Routes = _routeService.GetAllRoutes().ToList();

         return View(twm);
      }

      [HttpPost]
      public IActionResult AddTraining(TrainingViewModel model)
      {

         if (!ModelState.IsValid)
         {
            return View(model);
         }

         //add training
         Training training = new Training
         {
            Title = model.Title.ToCapital(),
            Description = model.Description,
            DateTime = model.Date.Add(model.Time.TimeOfDay),
            RouteId = model.RouteId,
            AvgPace = model.AvgPace,
            AddedDate = DateTime.UtcNow,
            ModifiedDate = DateTime.UtcNow
         };

         long trainingId = _trainingService.InsertTraining(training);

         //add related image if any; else do not bother now. 
         //TODO: Later give an error
         if (model.Photo != null)
         {
            string fileName = model.Photo.FileName;
            string contentType = model.Photo.ContentType;

            Stream stream = model.Photo.OpenReadStream();
            BinaryReader reader = new BinaryReader(stream);
            byte[] content = reader.ReadBytes((int)model.Photo.Length);

            TrainingPhoto photo = new TrainingPhoto
            {
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
      public IActionResult EditTraining(TrainingViewModel model)
      {

         if (!ModelState.IsValid)
         {
            return View(model);
         }

         //get training
         Training training = _trainingService.GetTrainingWithId(model.Id).FirstOrDefault();

         if (training == null)
         {
            ModelState.AddModelError("Error", "No such data!");
            return View(model);
         }

         training.Title = model.Title.ToCapital();
         training.Description = model.Description;
         training.DateTime = model.Date.Add(model.Time.TimeOfDay);
         training.AvgPace = model.AvgPace;
         training.RouteId = model.RouteId;
         training.ModifiedDate = DateTime.UtcNow;

         _trainingService.UpdateTraining(training);

         //delete old add new photo if any provided; else do not bother. 
         if (model.Photo != null)
         {
            string fileName = model.Photo.FileName;
            string contentType = model.Photo.ContentType;

            Stream stream = model.Photo.OpenReadStream();
            BinaryReader reader = new BinaryReader(stream);
            byte[] content = reader.ReadBytes((int)model.Photo.Length);

            //delete existing training photo
            TrainingPhoto photo = _trainingPhotoService.GetPhotoWithForeignKey(model.Id);
            if (photo != null)
            {
               _trainingPhotoService.DeletePhoto(photo);
            }

            //add new
            photo = new TrainingPhoto
            {
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

      #region Routes
      public IActionResult Routes()
      {
         //read all trainings so far and send to the page
         List<RouteViewModel> routes = _routeService.GetAllRoutes()
             .Select(e => new RouteViewModel
             {
                Id = e.Id,
                Name = e.Name,
                Distance = e.Distance,
                Latitude = e.Latitude,
                Longitude = e.Longitude,
                FileName = e.FileName,
                PhotoFileName = e.PhotoFileName
             }).OrderBy(e => e.Name).ToList();

         return View(routes);

      }
      public IActionResult AddRoute()
      {
         return View();

      }
      public IActionResult EditRoute(long routeId)
      {
         RouteViewModel rwm = _routeService.GetRouteWithId(routeId)
            .Select(t => new RouteViewModel
            {
               Id = t.Id,
               Name = t.Name,
               Latitude = t.Latitude,
               Longitude = t.Longitude,
               Distance = t.Distance
            }).FirstOrDefault();

         return View(rwm);
      }

      [HttpPost]
      public IActionResult AddRoute(RouteViewModel model)
      {

         if (!ModelState.IsValid)
         {
            return View(model);
         }

         string fileName = null;
         string photoFileName = null;
         string contentType = null;
         string photoContentType = null;
         byte[] content = null;
         byte[] photoContent = null;

         //add related route file if any; else do not bother now. 
         //TODO: Later give an error
         if (model.RouteFile != null)
         {
            fileName = model.RouteFile.FileName;
            contentType = model.RouteFile.ContentType;

            Stream stream = model.RouteFile.OpenReadStream();
            BinaryReader reader = new BinaryReader(stream);
            content = reader.ReadBytes((int)model.RouteFile.Length);
         }

         if (model.RoutePhotoFile != null)
         {
            photoFileName = model.RoutePhotoFile.FileName;
            photoContentType = model.RoutePhotoFile.ContentType;

            Stream stream = model.RoutePhotoFile.OpenReadStream();
            BinaryReader reader = new BinaryReader(stream);
            photoContent = reader.ReadBytes((int)model.RoutePhotoFile.Length);
         }

         //add training
         Route route = new Route
         {
            Name = model.Name.ToCapital(),
            Latitude = model.Latitude,
            Longitude = model.Longitude,
            Distance = model.Distance,
            Content = content,
            PhotoContent = photoContent,
            FileName = fileName,
            PhotoFileName = photoFileName,
            ContentType = contentType,
            PhotoContentType = photoContentType,
            AddedDate = DateTime.UtcNow,
            ModifiedDate = DateTime.UtcNow
         };

         long routeId = _routeService.InsertRoute(route);
         _routeService.SaveRouteChanges();

         return RedirectToAction("Routes");

      }

      [HttpPost]
      public IActionResult EditRoute(RouteViewModel model)
      {

         if (!ModelState.IsValid)
         {
            return View(model);
         }

         //get training
         Route route = _routeService.GetRouteWithId(model.Id).FirstOrDefault();

         if (route == null)
         {
            ModelState.AddModelError("Error", "No such data!");
            return View(model);
         }

         route.Name = model.Name.ToCapital();
         route.Latitude = model.Latitude;
         route.Longitude = model.Longitude;
         route.Distance = model.Distance;
         route.ModifiedDate = DateTime.UtcNow;

         //delete old add new photo if any provided; else do not bother. 
         if (model.RouteFile != null)
         {

            Stream stream = model.RouteFile.OpenReadStream();
            BinaryReader reader = new BinaryReader(stream);
            route.FileName = model.RouteFile.FileName;
            route.ContentType = model.RouteFile.ContentType;
            route.Content = reader.ReadBytes((int)model.RouteFile.Length);
         }

         if (model.RoutePhotoFile != null)
         {

            Stream stream = model.RoutePhotoFile.OpenReadStream();
            BinaryReader reader = new BinaryReader(stream);
            route.PhotoFileName = model.RoutePhotoFile.FileName;
            route.PhotoContentType = model.RoutePhotoFile.ContentType;
            route.PhotoContent = reader.ReadBytes((int)model.RoutePhotoFile.Length);
         }

         _routeService.UpdateRoute(route);
         _routeService.SaveRouteChanges();

         return RedirectToAction("Routes");
      }

      #endregion

      #region Events

 
      public IActionResult Events()
      {
         //read all trainings so far and send to the page
         List<EventViewModel> events = _eventService.GetAllEvents()
             .Select(e => new EventViewModel
             {
                Id = e.Id,
                Title = e.Title,
                DateStart = e.DateTimeStart,
                DateEnd = e.DateTimeEnd,
                Description = e.Description,
                Location = e.Location,
                PhotoName = e.EventPhoto.Name
             }).OrderByDescending(e => e.DateStart).ToList();

         return View(events);
      }

      public IActionResult AddEvent()
      {
         return View();
      }

      public IActionResult EditEvent(long eventId)
      {
         EventViewModel ewm = _eventService.GetEventWithId(eventId)
            .Select(t => new EventViewModel
            {
               Id = t.Id,
               Title = t.Title,
               Description = t.Description,
               Location = t.Location,
               DateStart = t.DateTimeStart,
               DateEnd = t.DateTimeEnd
            }).FirstOrDefault();


         //date and time in turkish format has problems so put them in viewbag
         ViewBag.DateStart = ewm.DateStart.ToString("yyyy-MM-dd");
         ViewBag.DateEnd = ewm.DateEnd?.ToString("yyyy-MM-dd");
         return View(ewm);
      }

      [HttpPost]
      public IActionResult AddEvent(EventViewModel model)
      {
         if (!ModelState.IsValid)
         {
            return View(model);
         }

         //add event
         Event e = new Event
         {
            Title = model.Title.ToCapital(),
            Description = model.Description,
            DateTimeStart = model.DateStart,
            DateTimeEnd = model.DateEnd,
            Location = model.Location,
            AddedDate = DateTime.UtcNow,
            ModifiedDate = DateTime.UtcNow
         };

         long eventId = _eventService.InsertEvent(e);

         //add related image if any; else do not bother now. 
         //TODO: Later give an error
         if (model.Photo != null)
         {
            string fileName = model.Photo.FileName;
            string contentType = model.Photo.ContentType;

            Stream stream = model.Photo.OpenReadStream();
            BinaryReader reader = new BinaryReader(stream);
            byte[] content = reader.ReadBytes((int)model.Photo.Length);

            EventPhoto photo = new EventPhoto
            {
               Name = model.Photo.FileName,
               ContentType = model.Photo.ContentType,
               Content = content,
               AddedDate = DateTime.UtcNow,
               ModifiedDate = DateTime.UtcNow,
               EventId = eventId
            };

            _eventPhotoService.InsertPhoto(photo);
         }

         _eventService.SaveEventChanges();
         _eventPhotoService.SavePhotoChanges();

         return RedirectToAction("Events");
      }

      [HttpPost]
      public IActionResult EditEvent(EventViewModel model)
      {

         if (!ModelState.IsValid)
         {
            return View(model);
         }

         //get training
         Event e = _eventService.GetEventWithId(model.Id).FirstOrDefault();

         if (e == null)
         {
            ModelState.AddModelError("Error", "No such data!");
            return View(model);
         }

         e.Title = model.Title.ToCapital();
         e.Description = model.Description;
         e.DateTimeStart = model.DateStart;
         e.DateTimeEnd = model.DateEnd;
         e.Location = model.Location;
         e.ModifiedDate = DateTime.UtcNow;

         _eventService.UpdateEvent(e);

         //delete old add new photo if any provided; else do not bother. 
         if (model.Photo != null)
         {
            string fileName = model.Photo.FileName;
            string contentType = model.Photo.ContentType;

            Stream stream = model.Photo.OpenReadStream();
            BinaryReader reader = new BinaryReader(stream);
            byte[] content = reader.ReadBytes((int)model.Photo.Length);

            //delete existing training photo
            EventPhoto photo = _eventPhotoService.GetPhotoWithForeignKey(model.Id);
            if (photo != null)
            {
               _eventPhotoService.DeletePhoto(photo);
            }

            //add new
            photo = new EventPhoto
            {
               Name = model.Photo.FileName,
               ContentType = model.Photo.ContentType,
               Content = content,
               AddedDate = DateTime.UtcNow,
               ModifiedDate = DateTime.UtcNow,
               EventId = model.Id
            };
            _eventPhotoService.InsertPhoto(photo);
         }

         _eventService.SaveEventChanges();
         _eventPhotoService.SavePhotoChanges();

         return RedirectToAction("Events");

      }

      #endregion

      public IActionResult Profiles()
      {
         ViewData["Message"] = "Your Profiles page.";
         return View();
      }

      public IActionResult Photos()
      {
         ViewData["Message"] = "Your photos description page.";
         return View();
      }

      public IActionResult Club()
      {
         ViewData["Message"] = "Your club description page.";
         return View();
      }

      public IActionResult Error()
      {
         return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
      }

      #region Download Methods

      public IActionResult DownloadRoute(long routeId)
      {

         Route route = _routeService.GetRouteWithId(routeId).FirstOrDefault();

         if (route == null)
         {
            return View("Error", new ErrorViewModel { ErrorMessage = "Not Found!" });
         }

         return File(route.Content, route.ContentType, route.Name);

      }

      public IActionResult DownloadRoutePhoto(long routeId)
      {

         Route route = _routeService.GetRouteWithId(routeId).FirstOrDefault();

         if (route == null)
         {
            return View("Error", new ErrorViewModel { ErrorMessage = "Not Found!" });
         }

         return File(route.PhotoContent, route.PhotoContentType, route.PhotoFileName);

      }

      public IActionResult DownloadTrainingPhoto(long trainingId)
      {

         TrainingPhoto photo = _trainingPhotoService.GetPhotoWithForeignKey(trainingId);

         if (photo == null)
         {
            return View("Error", new ErrorViewModel { ErrorMessage = "Not Found!" });
         }

         return File(photo.Content, photo.ContentType, photo.Name);

      }

      public IActionResult DownloadEventPhoto(long eventId)
      {

         EventPhoto photo = _eventPhotoService.GetPhotoWithForeignKey(eventId);

         if (photo == null)
         {
            return View("Error", new ErrorViewModel { ErrorMessage = "Not Found!" });
         }

         return File(photo.Content, photo.ContentType, photo.Name);

      }

      #endregion
   }
}
