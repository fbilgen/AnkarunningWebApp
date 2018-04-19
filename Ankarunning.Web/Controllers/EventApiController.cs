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
   [Produces("application/json")]
   [Route("api/events")]
   public class EventApiController : Controller
   {

      //DI
      private readonly IEventService<Event> _eventService;
      private readonly IPhotoService<EventPhoto> _eventPhotoService;
      private readonly IRouteService<Route> _routeService;
      //constructor DI
      public EventApiController(
         IEventService<Event> eventService,
         IPhotoService<EventPhoto> eventPhotoService,
         IRouteService<Route> routeService)
      {
         _eventService = eventService;
         _eventPhotoService = eventPhotoService;
         _routeService = routeService;
      }

      // GET: api/<controller>
      [HttpGet]
      public IEnumerable<EventViewModel> Get()
      {
         var events = _eventService.GetAllEvents().Select(t => new EventViewModel
         {
            Id = t.Id,
            Title = t.Title,
            Description = t.Description,
            DateStart = t.DateTimeStart,
            DateEnd = t.DateTimeEnd,
            Location = t.Location
         }).ToList();

         return events;
      }

      // GET api/<controller>/5
      [HttpGet("{id}")]
      public string Get(int id)
      {
         return "value";
      }

      // POST api/<controller>
      [HttpPost]
      public void Post([FromBody]string value)
      {
      }

      // PUT api/<controller>/5
      [HttpPut("{id}")]
      public void Put(int id, [FromBody]string value)
      {
      }

      // DELETE api/<controller>/5
      [HttpDelete("{id}")]
      public void Delete(int id)
      {
      }
   }
}
