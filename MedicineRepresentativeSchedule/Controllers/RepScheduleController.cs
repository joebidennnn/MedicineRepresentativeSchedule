using MedicineRepresentativeSchedule.Models;
using MedicineRepresentativeSchedule.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineRepresentativeSchedule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RepScheduleController : ControllerBase
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(RepScheduleController));
        public IRepScheduleService _repScheduleService { get; set; }
        public RepScheduleController(IRepScheduleService repScheduleService)
        {
            _repScheduleService = repScheduleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSchedule(DateTime ScheduleStartDate)
        {

            try
            {
                _log.Info("Calling CreateRepSchedule Method");
                IEnumerable<RepSchedule> Schedule=await _repScheduleService.CreateRepSchedule(ScheduleStartDate);
                if (Schedule == null)
                {
                    _log.Info("Null Returned.");
                    return NotFound("try again later");
                }
                else
                {
                    _log.Info("returned Schedule");
                    return Ok(Schedule);
                }
            }
            catch(ArgumentException argumentException)
            {
                _log.Error(argumentException);
                return BadRequest("Schedule start date is not correct! please enter valid date");
            }
            catch(Exception exception)
            {
                _log.Error(exception);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
