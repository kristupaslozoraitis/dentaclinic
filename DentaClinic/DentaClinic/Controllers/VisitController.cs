using DentaClinic.Models;
using DentaClinic.Models.Dtos;
using DentaClinic.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DentaClinic.Controllers
{
    [ApiController]
    [Route("api/v1/patientCards/{patientCardId}/visits")]
    public class VisitController : ControllerBase
    {
        private readonly IPatientCardRepository _patientCardsRepository;
        private readonly IAuthorizationService _authorizationService;
        private readonly IFreeVisitRepository _freeVisits;
        private readonly IVisitRepository _visits;

        public VisitController(IFreeVisitRepository freeVisits, IVisitRepository visits, IPatientCardRepository patientCardRepository, IAuthorizationService authorizationService)
        {
            _patientCardsRepository = patientCardRepository;
            _authorizationService = authorizationService;
            _freeVisits = freeVisits;
            _visits = visits;

        }

        [HttpGet]
        [Authorize(Roles = Roles.RegisteredUser)]
        public async Task<ActionResult<IEnumerable<VisitDto>>> GetAll(string patientCardId)
        {
            var card = await _patientCardsRepository.Get(patientCardId);
            if (card == null) return NotFound();

            var authResult = await _authorizationService.AuthorizeAsync(User, card, PolicyNames.ResourceOwner);
            if (!authResult.Succeeded)
            {
                return Forbid();
            }

            var visits = await _visits.GetAll(patientCardId);

            return Ok(visits.Select(visit => new VisitDto
            {
                Id = visit.Id,
                Date = visit.Date,
                Time = visit.Time,
                DoctorFullName = string.Format("{0} {1}", visit.DoctorName, visit.DoctorSurname),
                Service = visit.Service,
            }));
        }

        [HttpGet("{id}")]
        [Authorize(Roles = Roles.RegisteredUser)]
        public async Task<ActionResult<VisitDto>> Get(int id, string patientCardId)
        {
            var card = await _patientCardsRepository.Get(patientCardId);
            if (card == null) return NotFound();

            var visit = await _visits.Get(id);
            if (visit == null) return NotFound();

            var authResult = await _authorizationService.AuthorizeAsync(User, visit, PolicyNames.ResourceOwner);
            if (!authResult.Succeeded)
            {
                return Forbid();
            }

            return Ok(new VisitDto
            {
                Id = visit.Id,
                Date = visit.Date,
                Time = visit.Time,
                DoctorFullName = string.Format("{0} {1}", visit.DoctorName, visit.DoctorSurname),
                Service = visit.Service,
            });
        }

        [HttpPost]
        [Authorize(Roles = Roles.RegisteredUser)]
        public async Task<ActionResult<VisitDto>> Post(VisitPostDto visitDto, string patientCardId)
        {
            var card = await _patientCardsRepository.Get(patientCardId);
            if (card == null) return NotFound();

            var authResult = await _authorizationService.AuthorizeAsync(User, card, PolicyNames.ResourceOwner);
            if (!authResult.Succeeded)
            {
                return Forbid();
            }

            var freeVisit = await _freeVisits.Get(visitDto.FreeVisitId);
            if (freeVisit == null) return NotFound();

            var visit = new Visit
            {
                Date = visitDto.Date,
                Time = visitDto.Time,
                DoctorName = visitDto.DoctorName,
                DoctorSurname = visitDto.DoctorSurname,
                Service = visitDto.Service,
                PatientCard = card,
                UserId = User.FindFirstValue(JwtRegisteredClaimNames.Sub),
                FreeVisit = freeVisit
            };

            await _visits.Create(visit);

            return Created($"/api/v1/patientsCards/{patientCardId}/visits/{visit.Id}", new VisitDto
            {
                Id = visit.Id,
                Date = visit.Date,
                Time = visit.Time,
                DoctorFullName = string.Format("{0} {1}", visit.DoctorName, visit.DoctorSurname),
                Service = visit.Service,
            });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = Roles.RegisteredUser)]
        public async Task<ActionResult<VisitDto>> Update(VisitUpdateDto visitDto, int id, string patientCardId)
        {
            var card = await _patientCardsRepository.Get(patientCardId);
            if (card == null) return NotFound();

            var visit = await _visits.Get(id);
            if (visit == null) return NotFound();

            var authResult = await _authorizationService.AuthorizeAsync(User, visit, PolicyNames.ResourceOwner);
            if (!authResult.Succeeded)
            {
                return Forbid();
            }

            visit.DoctorName = visitDto.DoctorName;
            visit.DoctorSurname = visitDto.DoctorSurname;
            visit.Service = visitDto.Service;
            visit.Date = visitDto.Time;

            await _visits.Update(visit);

            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Roles.RegisteredUser)]
        public async Task<ActionResult<VisitDto>> Delete(int id, string patientCardId)
        {
            var card = await _patientCardsRepository.Get(patientCardId);
            if (card == null) return NotFound();

            var visit = await _visits.Get(id);
            if (visit == null) return NotFound();

            var authResult = await _authorizationService.AuthorizeAsync(User, visit, PolicyNames.ResourceOwner);
            if (!authResult.Succeeded)
            {
                return Forbid();
            }

            await _visits.Delete(visit);

            return NoContent();
        }
    }
}
