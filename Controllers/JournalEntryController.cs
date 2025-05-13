using AdhdJournalApi.Data;
using AdhdJournalApi.Models;
using AdhdJournalApi.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AdhdJournalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JournalEntryController : ControllerBase
    {
        private IJournalEntryService _entryService;

        public JournalEntryController(IJournalEntryService entryService)
        {
            _entryService = entryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEntries()
        {
            JournalEntryModel[] entries = await _entryService.GetAllEntries();
            return Ok(entries);
        }

        // POST api/<JournalEntry>
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Post([FromBody] JournalEntryModel model)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest();
            }

            try
            {
                await _entryService.SaveToDbAsync(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEntry(int id, [FromBody] JournalEntryModel model)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _entryService.UpdateEntry(id, model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEntry(int id)
        {
            JournalEntryModel entry;
            try
            {
                entry = await _entryService.GetEntry(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(entry);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _entryService.DeleteEntry(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NoContent();
        }
    }
}
