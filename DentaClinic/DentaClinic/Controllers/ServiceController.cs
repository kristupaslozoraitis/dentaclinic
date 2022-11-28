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
    [Route("api/v1/services")]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceRepository _services;
        private readonly IAuthorizationService _authorizationService;

        public ServiceController(IServiceRepository services, IAuthorizationService authorizationService)
        {
            _services = services;
            _authorizationService = authorizationService;
        }

        [HttpGet]
        [Authorize(Roles = Roles.Odontologist)]
        public async Task<IEnumerable<ServiceDto>> GetAll()
        {
            var services = await _services.GetAll();

            return services.Select(service => new ServiceDto
            {
                Name = service.Name,
            });
        }

        [HttpGet("{id}")]
        [Authorize(Roles = Roles.Odontologist)]
        public async Task<ActionResult<ServiceDto>> Get(int id)
        {
            var service = await _services.Get(id);
            if (service == null) return NotFound();

            var authResult = await _authorizationService.AuthorizeAsync(User, service, PolicyNames.ResourceOwner);
            if (!authResult.Succeeded)
            {
                return Forbid();
            }

            return Ok(new ServiceDto
            {
                Name = service.Name
            });
        }

        [HttpPost]
        [Authorize(Roles = Roles.Odontologist)]
        public async Task<ActionResult<ServiceDto>> Post(ServicePostDto service)
        {
            var newService = new Service
            {
                Name = service.Name,
                UserId = User.FindFirstValue(JwtRegisteredClaimNames.Sub)
            };

            await _services.Create(newService);

            return Created($"/api/v1/services/{newService.Id}", new ServiceDto
            {
                Name = newService.Name
            });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = Roles.Odontologist)]
        public async Task<ActionResult<ServiceDto>> Update(ServicePostDto serviceDto, int id)
        {
            var service = await _services.Get(id);
            if (service == null) return NotFound();

            var authResult = await _authorizationService.AuthorizeAsync(User, service, PolicyNames.ResourceOwner);
            if (!authResult.Succeeded)
            {
                return Forbid();
            }

            service.Name = serviceDto.Name;

            await _services.Update(service);

            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Roles.Odontologist)]
        public async Task<ActionResult<ServiceDto>> Delete(int id)
        {
            var service = await _services.Get(id);
            if (service == null) return NotFound();

            await _services.Delete(service);

            return NoContent();
        }
    }
}
