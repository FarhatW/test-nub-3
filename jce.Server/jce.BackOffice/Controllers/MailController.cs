using jce.Common.Resources;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jce.BusinessLayer.IManagers;

namespace jce.BackOffice.Controllers
{
    [Produces("application/json")]
    [Route("api/mail")]
    public class MailController : Controller
    {
        private readonly IMailManager _mailManager;

        public MailController(IMailManager mailManager)
        {
            _mailManager = mailManager;
        }

        //[HttpGet]
        //public async Task<IActionResult> GetSchedules(ScheduleQueryResource filtesQuery)
        //{
        //    var schedules = await _scheduleManager.GetAll(filtesQuery);
        //    if (schedules == null)
        //        return NotFound();


        //    return Ok(schedules);
        //}

        //[HttpGet("{id}")]
        //public async Task<IActionResult> getScheduleById(int id)
        //{
        //    var schedules = await _scheduleManager.GetItemById(id);
        //    if (schedules == null)
        //        return NotFound();

        //    return Ok(schedules);
        //}

        [HttpPost]
        public async Task<IActionResult> Add([FromBody]MailSaveResource ScheduleResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _mailManager.Add(ScheduleResource);
            return Ok(result);
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> Update(int id, [FromBody]ScheduleSaveResource ScheduleResource)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    var result = await _scheduleManager.Update(id, ScheduleResource);

        //    if (result == null)
        //        return NotFound();

        //    return Ok(result);
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteRole(int id)
        //{
        //    await _scheduleManager.Delete(id);

        //    return Ok(id);
        //}
    }
}
