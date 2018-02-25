using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ankarunning.Data;
using Ankarunning.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ankarunning.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/training")]
    public class TrainingApiController : Controller
    {
        //DI
        private readonly ITrainingService<Training> _trainingService;
        private readonly IPhotoService<TrainingPhoto> _trainingPhotoService;
        //constructor DI
        public TrainingApiController(ITrainingService<Training> trainingService,
            IPhotoService<TrainingPhoto> trainingPhotoService) {
            _trainingService = trainingService;
            _trainingPhotoService = trainingPhotoService;
        }

        // GET: api/Training
        [HttpGet]
        public IEnumerable<Training> Get()
        {
            return _trainingService.GetAllTrainings().ToList();
        }

        // GET: api/Training/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            return new ObjectResult( _trainingService.GetTrainingWithId(id)); 
            //can also use specifically Training as return type

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
