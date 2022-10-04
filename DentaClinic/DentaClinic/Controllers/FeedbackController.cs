using DentaClinic.Models;
using DentaClinic.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DentaClinic.Controllers
{
    [ApiController]
    [Route("api/v1/patientCards/{patientCardId}/visits/{visitId}/feedbacks")]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackRepository _feedbacks;
        private readonly IVisitRepository _visits;
        private readonly IPatientCardRepository _patientCards;

        public FeedbackController(IFeedbackRepository feedbacks, IVisitRepository visits, IPatientCardRepository patientCards)
        {
            _feedbacks = feedbacks;
            _visits = visits;
            _patientCards = patientCards;
        }

        [HttpGet]
        public async Task<ActionResult<List<VisitFeedback>>> GetAll(int patientCardId, int visitId)
        {
            var card = await _patientCards.Get(patientCardId);
            if (card == null) return NotFound();

            var visit = await _visits.Get(visitId);
            if (visit == null) return NotFound();

            var feedbacks = await _feedbacks.GetAll(visitId);

            return Ok(feedbacks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VisitFeedback>> Get(int patientCardId, int visitId, int id)
        {
            var card = await _patientCards.Get(patientCardId);
            if (card == null) return NotFound();

            var visit = await _visits.Get(visitId);
            if (visit == null) return NotFound();

            var feedback = await _feedbacks.Get(id);

            return Ok(feedback);
        }

        [HttpPost]
        public async Task<ActionResult<VisitFeedback>> Post(int patientCardId, int visitId, VisitFeedback visitFeedback)
        {
            var card = await _patientCards.Get(patientCardId);
            if (card == null) return NotFound();

            var _visit = await _visits.Get(visitId);
            if (_visit == null) return NotFound();

            await _feedbacks.Create(visitFeedback);

            return Created($"api/v1/patientCards/{patientCardId}/visits/{visitId}/feedbacks/{visitFeedback.Id}", visitFeedback);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<VisitFeedback>> Update(int patientCardId, int visitId, VisitFeedback visitFeedback, int id)
        {
            var card = await _patientCards.Get(patientCardId);
            if (card == null) return NotFound();

            var _visit = await _visits.Get(visitId);
            if (_visit == null) return NotFound();

            var _visitFeedback = await _feedbacks.Get(id);
            if (_visitFeedback == null) return NotFound();

            await _feedbacks.Update(visitFeedback);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<VisitFeedback>> Delete(int patientCardId, int visitId, int id)
        {
            var card = await _patientCards.Get(patientCardId);
            if (card == null) return NotFound();

            var _visit = await _visits.Get(visitId);
            if (_visit == null) return NotFound();

            var _visitFeedback = await _feedbacks.Get(id);
            if (_visitFeedback == null) return NotFound();

            await _feedbacks.Delete(_visitFeedback);

            return NoContent();
        }
    }
}
