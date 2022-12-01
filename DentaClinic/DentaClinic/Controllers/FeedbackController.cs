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
    [Route("api/v1/patientCards/{patientCardId}/visits/{visitId}/feedbacks")]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackRepository _feedbacks;
        private readonly IVisitRepository _visits;
        private readonly IPatientCardRepository _patientCards;
        private readonly IAuthorizationService _authorizationService;

        public FeedbackController(IFeedbackRepository feedbacks, IVisitRepository visits, IPatientCardRepository patientCards, IAuthorizationService authorizationService)
        {
            _feedbacks = feedbacks;
            _visits = visits;
            _patientCards = patientCards;
            _authorizationService = authorizationService;
        }

        [HttpGet]
        [Authorize(Roles = Roles.RegisteredUser)]
        public async Task<ActionResult<IEnumerable<VisitFeedbackDto>>> GetAll(string patientCardId, int visitId)
        {
            var card = await _patientCards.Get(patientCardId);
            if (card == null) return NotFound();

            var visit = await _visits.Get(visitId);
            if (visit == null) return NotFound();

            var feedbacks = await _feedbacks.GetAll(visitId);

            return Ok(feedbacks.Select(feedback => new VisitFeedbackDto
            {
                Id = feedback.Id,
                Feedback = feedback.Feedback,
            }));
        }

        [HttpGet("{id}")]
        [Authorize(Roles = Roles.RegisteredUser)]
        public async Task<ActionResult<VisitFeedbackDto>> Get(string patientCardId, int visitId, int id)
        {
            var card = await _patientCards.Get(patientCardId);
            if (card == null) return NotFound();

            var visit = await _visits.Get(visitId);
            if (visit == null) return NotFound();

            var feedback = await _feedbacks.Get(id);

            return Ok(new VisitFeedbackDto
            {
                Id = feedback.Id,
                Feedback = feedback.Feedback,
            });
        }

        [HttpPost]
        [Authorize(Roles = Roles.RegisteredUser)]
        public async Task<ActionResult<VisitFeedbackDto>> Post(string patientCardId, int visitId, VisitFeedbackPostDto visitFeedback)
        {
            var card = await _patientCards.Get(patientCardId);
            if (card == null) return NotFound();

            var _visit = await _visits.Get(visitId);
            if (_visit == null) return NotFound();

            var authResult = await _authorizationService.AuthorizeAsync(User, _visit, PolicyNames.ResourceOwner);
            if (!authResult.Succeeded)
            {
                return Forbid();
            }

            var newFeedback = new VisitFeedback
            {
                Feedback = visitFeedback.Feedback,
                Visit = _visit,
                UserId = User.FindFirstValue(JwtRegisteredClaimNames.Sub)
            };

            await _feedbacks.Create(newFeedback);

            return Created($"api/v1/patientCards/{patientCardId}/visits/{visitId}/feedbacks/{newFeedback.Id}", new VisitFeedbackDto
            {
                Id = newFeedback.Id,
                Feedback = newFeedback.Feedback
            });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = Roles.RegisteredUser)]
        public async Task<ActionResult<VisitFeedbackDto>> Update(string patientCardId, int visitId, VisitFeedbackUpdateDto visitFeedbackUpdateDto, int id)
        {
            var card = await _patientCards.Get(patientCardId);
            if (card == null) return NotFound();

            var _visit = await _visits.Get(visitId);
            if (_visit == null) return NotFound();

            var visitFeedback = await _feedbacks.Get(id);
            if (visitFeedback == null) return NotFound();

            var authResult = await _authorizationService.AuthorizeAsync(User, visitFeedback, PolicyNames.ResourceOwner);
            if (!authResult.Succeeded)
            {
                return Forbid();
            }

            visitFeedback.Feedback = visitFeedbackUpdateDto.Feedback;

            await _feedbacks.Update(visitFeedback);

            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Roles.RegisteredUser)]
        public async Task<ActionResult<VisitFeedback>> Delete(string patientCardId, int visitId, int id)
        {
            var card = await _patientCards.Get(patientCardId);
            if (card == null) return NotFound();

            var _visit = await _visits.Get(visitId);
            if (_visit == null) return NotFound();

            var _visitFeedback = await _feedbacks.Get(id);
            if (_visitFeedback == null) return NotFound();

            var authResult = await _authorizationService.AuthorizeAsync(User, _visitFeedback, PolicyNames.ResourceOwner);
            if (!authResult.Succeeded)
            {
                return Forbid();
            }

            await _feedbacks.Delete(_visitFeedback);

            return NoContent();
        }
    }
}
