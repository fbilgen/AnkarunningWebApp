using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ankarunning.Data;
using Ankarunning.Service;
using Ankarunning.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ankarunning.Web.Controllers
{
   [Produces("application/json")]
   [Route("api/trainings")]
   public class TrainingApiController : Controller
   {
      //DI
      private readonly ITrainingService<Training> _trainingService;
      private readonly IPhotoService<TrainingPhoto> _trainingPhotoService;
      private readonly IRouteService<Route> _routeService;
      //constructor DI
      public TrainingApiController(
         ITrainingService<Training> trainingService,
         IPhotoService<TrainingPhoto> trainingPhotoService,
         IRouteService<Route> routeService)
      {
         _trainingService = trainingService;
         _trainingPhotoService = trainingPhotoService;
         _routeService = routeService;
      }

      // GET: api/training
      [HttpGet]
      public IEnumerable<TrainingViewModel> GetAll()
      {
         var trainings = _trainingService.GetAllTrainings().Select(t => new TrainingViewModel
         {
            Title = t.Title,
            Description = t.Description,
            DateTime = t.DateTime,
            Route = t.Route.Name,
            Distance = t.Route.Distance,
            AvgPace = t.AvgPace
         }).ToList();

         return trainings;
      }

      // GET: api/trainings/5
      [HttpGet("{id}", Name = "GetTraining")]
      public IActionResult GetById(long id)
      {
         TrainingViewModel twm = _trainingService.GetTrainingWithId(id)
         .Select(t => new TrainingViewModel
         {
            Id = t.Id,
            Title = t.Title,
            Description = t.Description,
            DateTime = t.DateTime,
            Route = t.Route.Name,
            Distance = t.Route.Distance,
            AvgPace = t.AvgPace
         }).FirstOrDefault();

         if (twm == null)
         {
            return NotFound();
         }
         return new ObjectResult(twm);

      }

      [HttpGet]
      [Route("future")]
      public TrainingViewModel GetFutureTraining()
      {
         TrainingViewModel ft = _trainingService.GetAllTrainings()
            .OrderByDescending(t => t.DateTime)
            .Select(t => new TrainingViewModel
            {
               Id = t.Id,
               Title = t.Title,
               Description = t.Description,
               AvgPace = t.AvgPace,
               DateTime = t.DateTime,
               Route = t.Route.Name,
               Distance = t.Route.Distance,
               RouteId = t.RouteId, //need this for calling map with this route
               TrainingPhoto = new TrainingPhotoViewModel
               {
                  Name = t.TrainingPhoto.Name,
                  Content = Convert.ToBase64String(t.TrainingPhoto.Content),
                  ContentType = t.TrainingPhoto.ContentType
               }
            }).FirstOrDefault();

         return ft;

      }

      // POST: api/Training
      [HttpPost]
      public void Post([FromBody]string value)
      {
         throw new NotImplementedException();
      }

      // PUT: api/Training/5
      [HttpPut("{id}")]
      public void Put(int id, [FromBody]string value)
      {
         throw new NotImplementedException();
      }

      // DELETE: api/ApiWithActions/5
      [HttpDelete("{id}")]
      public void Delete(int id)
      {
         throw new NotImplementedException();
      }
   }
}
