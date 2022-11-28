using DentaClinic.Models.Dtos;
using DentaClinic.Models;
using DentaClinic.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace DentaClinic.Controllers
{
    [ApiController]
    [Route("api/v1/freeVisits")]
    public class FreeVisitController : ControllerBase
    {
        private readonly IFreeVisitRepository _freeVisits;
        private readonly IAuthorizationService _authorizationService;
        private readonly IServiceRepository _services;

        public FreeVisitController(IFreeVisitRepository freeVisits, IAuthorizationService authorizationService, IServiceRepository services)
        {
            _freeVisits = freeVisits;
            _authorizationService = authorizationService;
            _services = services;
        }

        [HttpGet]
        [Authorize(Roles = Roles.RegisteredUser)]
        public async Task<IEnumerable<FreeVisitDto>> GetAll()
        {
            var freeVisits = await _freeVisits.GetAll();

            return freeVisits.Select(freeVisit => new FreeVisitDto
            {
                Date = freeVisit.Date,
                Time = freeVisit.Time,
                UserId = freeVisit.UserId,
                DoctorFullName = string.Format("{0} {1}", freeVisit.User.FirstName, freeVisit.User.LastName),
                ServiceId = freeVisit.Service.Id,
                Service = freeVisit.Service.Name
            });
        }

        [HttpGet("{id}")]
        [Authorize(Roles = Roles.RegisteredUser)]
        public async Task<ActionResult<FreeVisitDto>> Get(int id)
        {
            var freeVisit = await _freeVisits.Get(id);
            if (freeVisit == null) return NotFound();

            var authResult = await _authorizationService.AuthorizeAsync(User, freeVisit, PolicyNames.ResourceOwner);
            if (!authResult.Succeeded)
            {
                return Forbid();
            }

            return Ok(new FreeVisitDto
            {
                Date = freeVisit.Date,
                Time = freeVisit.Time,
                UserId = freeVisit.UserId,
                DoctorFullName = string.Format("{0} {1}", freeVisit.User.FirstName, freeVisit.User.LastName),
                ServiceId = freeVisit.Service.Id,
                Service = freeVisit.Service.Name
            });
        }

        [HttpPost]
        [Authorize(Roles = Roles.Odontologist)]
        public async Task<ActionResult<ServiceDto>> Post(FreeVisitPostDto freeVisit)
        {
            var service = await _services.Get(freeVisit.ServiceId);
            if (service == null) return NotFound();

            var newFreeVisit = new FreeVisit
            {
                Date = freeVisit.Date,
                Time = freeVisit.Time,
                UserId = User.FindFirstValue(JwtRegisteredClaimNames.Sub),
                Service = service,
            };

            await _freeVisits.Create(newFreeVisit);

            return Created($"/api/v1/freeVisits/{newFreeVisit.Id}", new FreeVisitDto
            {
                Date = newFreeVisit.Date,
                Time = newFreeVisit.Time,
                UserId = newFreeVisit.UserId,
                DoctorFullName = string.Format("{0} {1}", newFreeVisit.User.FirstName, newFreeVisit.User.LastName),
                ServiceId = newFreeVisit.Service.Id,
                Service = newFreeVisit.Service.Name
            });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = Roles.Odontologist)]
        public async Task<ActionResult<FreeVisitDto>> Update(FreeVisitUpdateDto freeVisitDto, int id)
        {
            var freeVisit = await _freeVisits.Get(id);
            if (freeVisit == null) return NotFound();

            var service = await _services.Get(freeVisitDto.ServiceId);
            if (service == null) return NotFound();

            var authResult = await _authorizationService.AuthorizeAsync(User, freeVisit, PolicyNames.ResourceOwner);
            if (!authResult.Succeeded)
            {
                return Forbid();
            }

            freeVisit.Date = freeVisitDto.Date;
            freeVisit.Time = freeVisitDto.Time;
            freeVisit.Service = service;

            await _freeVisits.Update(freeVisit);

            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Roles.Odontologist)]
        public async Task<ActionResult<FreeVisitDto>> Delete(int id)
        {
            var freeVisit = await _freeVisits.Get(id);
            if (freeVisit == null) return NotFound();

            await _freeVisits.Delete(freeVisit);

            return NoContent();
        }
    }
}
