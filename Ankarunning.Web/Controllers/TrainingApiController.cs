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
      private readonly IParameterService<TrainingPlace> _trainingPlaceService;
      //constructor DI
      public TrainingApiController(
         ITrainingService<Training> trainingService,
         IPhotoService<TrainingPhoto> trainingPhotoService,
         IParameterService<TrainingPlace> trainingPlaceService)
      {
         _trainingService = trainingService;
         _trainingPhotoService = trainingPhotoService;
         _trainingPlaceService = trainingPlaceService;
      }

      // GET: api/trainings
      [HttpGet]
      public IEnumerable<TrainingViewModel> GetAll()
      {
         var trainings = _trainingService.GetAllTrainings().Select(e => new TrainingViewModel
         {
            Title = e.Title,
            Place = e.TrainingPlace.Name,
            Description = e.Description,
            DateTime = e.DateTime,
            Distance = e.Distance
            //TrainingPhoto = new TrainingPhotoViewModel
            //{
            //   Name = e.TrainingPhoto.Name,
            //   Content = Convert.ToBase64String(e.TrainingPhoto.Content),
            //   ContentType = e.TrainingPhoto.ContentType
            //}
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
            Place = t.TrainingPlace.Name,
            Description = t.Description,
            Distance = t.Distance,
            Date = t.DateTime,
            Time = t.DateTime,
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
            .OrderByDescending(e => e.DateTime)
            .Select(e => new TrainingViewModel
            {
               Id = e.Id,
               Title = e.Title,
               Place = e.TrainingPlace.Name,
               Description = e.Description,
               Distance = e.Distance,
               DateTime = e.DateTime,
               TrainingPhoto = new TrainingPhotoViewModel
               {
                  Name = e.TrainingPhoto.Name,
                  Content = Convert.ToBase64String(e.TrainingPhoto.Content),
                  ContentType = e.TrainingPhoto.ContentType
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
