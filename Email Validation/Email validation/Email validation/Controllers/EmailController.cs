using Microsoft.AspNetCore.Mvc;
using EmailValidationAPI.Services;

namespace EmailValidationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly EmailValidator _validator;

        public EmailController(EmailValidator validator)
        {
            _validator = validator;
        }

        [HttpGet("validate")]
        public IActionResult ValidateEmail(string email)
        {
            if (!_validator.IsValidFormat(email))
                return BadRequest("Invalid email format.");

            bool isDeliverable = _validator.IsEmailDeliverable(email);

            return Ok(new
            {
                email = email,
                format = true,
                deliverable = isDeliverable
            });
        }
    }
}