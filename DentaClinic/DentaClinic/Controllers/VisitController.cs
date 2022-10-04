using DentaClinic.Models;
using DentaClinic.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DentaClinic.Controllers
{
    [ApiController]
    [Route("api/v1/patientCards/{patientCardId}/visits")]
    public class VisitController : ControllerBase
    {
        private readonly IVisitRepository _visits;

        public VisitController(IVisitRepository visits)
        {
            _visits = visits;
        }

        [HttpGet]
        public async Task<List<Visit>> GetAll(int patientCardId)
        {
            return await _visits.GetAll(patientCardId);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Visit>> Get(int id)
        {
            var visit = await _visits.Get(id);
            if (visit == null) return NotFound();

            return Ok(visit);
        }

        [HttpPost]
        public async Task<ActionResult<Visit>> Post(Visit visit)
        {
            await _visits.Create(visit);

            return Created($"/api/v1/patientsCards/{visit.Id}", visit);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Visit>> Update(Visit visit, int id)
        {
            var _visit = _visits.Get(id);
            if (_visit == null) return NotFound();

            await _visits.Update(visit);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Visit>> Delete(int id)
        {
            var visit = await _visits.Get(id);
            if (visit == null) return NotFound();

            await _visits.Delete(visit);

            return NoContent();
        }
    }
}
