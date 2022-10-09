using DentaClinic.Models;
using DentaClinic.Models.Dtos;
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
        public async Task<IEnumerable<PatientCardDto>> GetAll()
        {
            var cards = await _patientCards.GetAll();

            return cards.Select(card => new PatientCardDto
            {
                Id = card.Id,
                Name = card.Name,
                Surname = card.Surname,
                BirthDate = card.BirthDate,
                PersonalNumber = card.PersonalNumber,
                HomeAddress = card.HomeAddress,
                PhoneNumber = card.PhoneNumber,
                Height = card.Height,
                Weight = card.Weight,
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PatientCardDto>> Get(int id)
        {
            var card = await _patientCards.Get(id);
            if (card == null) return NotFound();

            return Ok(new PatientCardDto
            {
                Id = card.Id,
                Name = card.Name,
                Surname = card.Surname,
                BirthDate = card.BirthDate,
                PersonalNumber = card.PersonalNumber,
                HomeAddress = card.HomeAddress,
                PhoneNumber = card.PhoneNumber,
                Height = card.Height,
                Weight = card.Weight,
            });
        }

        [HttpPost]
        public async Task<ActionResult<PatientCardDto>> Post(PatientCardPostDto card)
        {
            var newCard = new PatientCard
            {
                Name = card.Name,
                Surname = card.Surname,
                BirthDate = card.BirthDate,
                PersonalNumber = card.PersonalNumber,
                HomeAddress = card.HomeAddress,
                PhoneNumber = card.PhoneNumber,
                Height = card.Height,
                Weight = card.Weight,
            };

            await _patientCards.Create(newCard);

            return Created($"/api/v1/patientsCards/{newCard.Id}", new PatientCardDto
            {
                Id = newCard.Id,
                Name = newCard.Name,
                Surname = newCard.Surname,
                BirthDate = newCard.BirthDate,
                PersonalNumber = newCard.PersonalNumber,
                HomeAddress = newCard.HomeAddress,
                PhoneNumber = newCard.PhoneNumber,
                Height = newCard.Height,
                Weight = newCard.Weight,
            });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PatientCard>> Update(PatientCardUpdateDto patientCardDto, int id)
        {
            var card = await _patientCards.Get(id);
            if (card == null) return NotFound();

            card.Height = patientCardDto.Height;
            card.Weight = patientCardDto.Weight;
            card.HomeAddress = patientCardDto.HomeAddress;
            card.PhoneNumber = patientCardDto.PhoneNumber;

            await _patientCards.Update(card);

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
