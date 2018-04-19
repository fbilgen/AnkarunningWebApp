using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ankarunning.Data;
using Ankarunning.Service;
using Ankarunning.Web.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ankarunning.Web.Controllers
{
    [Route("api/routes")]
    public class RouteApiController : Controller
    {
      //DI
      private readonly ITrainingService<Training> _trainingService;
      private readonly IRouteService<Route> _routeService;
      //constructor DI
      public RouteApiController(
         ITrainingService<Training> trainingService,
         IRouteService<Route> routeService)
      {
         _trainingService = trainingService;
         _routeService = routeService;
      }

      // GET: api/<controller>
      [HttpGet]
        public IEnumerable<RouteViewModel> GetAll()
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
             }).OrderBy(e => e.Name).ToList();

         return routes;

      }

      // GET: api/trainings/5
      [HttpGet("{id}", Name = "GetRoute")]
      public IActionResult GetById(long id)
      {
         RouteViewModel rwm = _routeService.GetRouteWithId(id)
         .Select(e => new RouteViewModel
         {
            Id = e.Id,
            Name = e.Name,
            Distance = e.Distance,
            Latitude = e.Latitude,
            Longitude = e.Longitude,
            FileName = e.FileName,
            Content =  e.Content,
            ContentType = e.ContentType
         }).FirstOrDefault();

         if (rwm == null)
         {
            return NotFound();
         }

         return new ObjectResult(rwm);

      }
    }
}
