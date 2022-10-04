using DentaClinic.Models;
using DentaClinic.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DentaClinic.Controllers
{
    [ApiController]
    [Route("api/v1/patientsCards")]
    public class PatientCardController : ControllerBase
    {
        private readonly IPatientCardRepository _patientCards;

        public PatientCardController(IPatientCardRepository patientCards)
        {
            _patientCards = patientCards;
        }

        [HttpGet]
        public async Task<List<PatientCard>> GetAll()
        {
            return await _patientCards.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PatientCard>> Get(int id)
        {
            var card = await _patientCards.Get(id);
            if (card == null) return NotFound();

            return Ok(card);
        }

        [HttpPost]
        public async Task<ActionResult<PatientCard>> Post(PatientCard card)
        {
            await _patientCards.Create(card);

            return Created($"/api/v1/patientsCards/{card.Id}", card);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PatientCard>> Update(PatientCard patientCard, int id)
        {
            var card = _patientCards.Get(id);
            if (card == null) return NotFound();

            await _patientCards.Update(patientCard);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<PatientCard>> Delete(int id)
        {
            var card = await _patientCards.Get(id);
            if (card == null) return NotFound();

            await _patientCards.Delete(card);

            return NoContent();
        }
    }
}
