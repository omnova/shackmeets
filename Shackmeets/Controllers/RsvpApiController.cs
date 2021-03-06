using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Shackmeets.Models;
using Shackmeets.Dtos;
using Shackmeets.Validators;
using System.Security.Claims;

namespace Shackmeets.Controllers
{
  [Route("api")]
  public class RsvpApiController : Controller
  {
    private readonly ShackmeetsDbContext dbContext;
    private readonly ILogger logger;

    public RsvpApiController(ShackmeetsDbContext context, ILogger<ShackmeetApiController> logger)
    {
      this.dbContext = context;
      this.logger = logger;
    }

    [HttpPost("[action]")]
    [Authorize]
    public IActionResult Rsvp([FromBody] RsvpDto rsvpDto)
    {
      try
      {
        this.logger.LogDebug("Rsvp");

        // Verify input exists
        if (rsvpDto == null)
        {
          return BadRequest(new BadInputResponse());
        }

        // Use the username of the authenticated user so they can only update their own RSVPs.
        var currentUsername = this.User.FindFirst(ClaimTypes.Name).Value;

        // Verify meet exists
        bool meetExists = this.dbContext.Meets.Any(m => m.MeetId == rsvpDto.MeetId);

        if (!meetExists)
        {
          return BadRequest(new ErrorResponse("Shackmeet does not exist."));
        }

        var rsvp = this.dbContext.Rsvps.SingleOrDefault(r => r.MeetId == rsvpDto.MeetId && r.Username == currentUsername);

        if (rsvp != null)
        {
          // Existing RSVP
          this.dbContext.Rsvps.Attach(rsvp);
        }
        else
        {
          // New RSVP
          rsvp = new Rsvp
          {
            MeetId = rsvpDto.MeetId,
            Username = currentUsername
          };

          this.dbContext.Add(rsvp);
        }

        rsvp.RsvpType = rsvpDto.RsvpType;
        rsvp.NumAttendees = rsvpDto.NumAttendees;

        // Validate fields
        var validator = new RsvpValidator();
        var validationResult = validator.Validate(rsvp);

        if (!validationResult.IsValid)
        {
          return BadRequest(new ValidationErrorResponse(validationResult.Messages));
        }

        this.dbContext.SaveChanges();

        return Ok(new SuccessResponse());
      }
      catch (Exception e)
      {
        // Log error
        this.logger.LogError("Message: {0}" + Environment.NewLine + "{1}", e.Message, e.StackTrace);

        return BadRequest(new CriticalErrorResponse());
      }
    }
  }
}
