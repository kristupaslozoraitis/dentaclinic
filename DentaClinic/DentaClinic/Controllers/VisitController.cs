using DentaClinic.Models;
using DentaClinic.Models.Dtos;
using DentaClinic.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DentaClinic.Controllers
{
    [ApiController]
    [Route("api/v1/patientCards/{patientCardId}/visits")]
    public class VisitController : ControllerBase
    {
        private readonly IPatientCardRepository _patientCardsRepository;
        private readonly IVisitRepository _visits;

        public VisitController(IVisitRepository visits, IPatientCardRepository patientCardRepository)
        {
            _patientCardsRepository = patientCardRepository;
            _visits = visits;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VisitDto>>> GetAll(int patientCardId)
        {
            var card = await _patientCardsRepository.Get(patientCardId);
            if (card == null) return NotFound();

            var visits = await _visits.GetAll(patientCardId);

            return Ok(visits.Select(visit => new VisitDto
            {
                Id = visit.Id,
                Time = visit.Time,
                DoctorName = visit.DoctorName,
                DoctorSurname = visit.DoctorSurname,
                Service = visit.Service,
            }));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VisitDto>> Get(int id, int patientCardId)
        {
            var card = await _patientCardsRepository.Get(patientCardId);
            if (card == null) return NotFound();

            var visit = await _visits.Get(id);
            if (visit == null) return NotFound();

            return Ok(new VisitDto
            {
                Id = visit.Id,
                Time = visit.Time,
                DoctorName = visit.DoctorName,
                DoctorSurname = visit.DoctorSurname,
                Service = visit.Service,
            });
        }

        [HttpPost]
        public async Task<ActionResult<VisitDto>> Post(VisitPostDto visitDto, int patientCardId)
        {
            var card = await _patientCardsRepository.Get(patientCardId);
            if (card == null) return NotFound();

            var visit = new Visit
            {
                Time = visitDto.Time,
                DoctorName = visitDto.DoctorName,
                DoctorSurname = visitDto.DoctorSurname,
                Service = visitDto.Service,
                PatientCard = card,
            };

            await _visits.Create(visit);

            return Created($"/api/v1/patientsCards/{patientCardId}/visits/{visit.Id}", new VisitDto
            {
                Id = visit.Id,
                Time = visit.Time,
                DoctorName = visit.DoctorName,
                DoctorSurname = visit.DoctorSurname,
                Service = visit.Service,
            });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<VisitDto>> Update(VisitUpdateDto visitDto, int id, int patientCardId)
        {
            var card = await _patientCardsRepository.Get(patientCardId);
            if (card == null) return NotFound();

            var visit = await _visits.Get(id);
            if (visit == null) return NotFound();

            visit.DoctorName = visitDto.DoctorName;
            visit.DoctorSurname = visitDto.DoctorSurname;
            visit.Service = visitDto.Service;
            visit.Time = visitDto.Time;

            await _visits.Update(visit);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<VisitDto>> Delete(int id, int patientCardId)
        {
            var card = await _patientCardsRepository.Get(patientCardId);
            if (card == null) return NotFound();

            var visit = await _visits.Get(id);
            if (visit == null) return NotFound();

            await _visits.Delete(visit);

            return NoContent();
        }
    }
}
