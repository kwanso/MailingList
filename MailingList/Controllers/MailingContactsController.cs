using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MailingList.Models;

namespace MailingList.Controllers
{
    [Route("api/MailingContacts")]
    [ApiController]
    public class MailingContactsController : ControllerBase
    {
        private readonly MailingContext _context;

        public MailingContactsController(MailingContext context)
        {
            _context = context;
        }

        // GET: api/MailingContacts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MailingContact>>> GetMailingContacts([FromQuery] string LastName = null, bool Ascending = true)
        {
            List<MailingContact> mailingContacts;
            if (LastName!=null && Ascending)
            {
                mailingContacts = await _context.MailingContacts.Where(c => c.LastName==LastName).OrderBy(c => c.LastName).ThenBy(c => c.FirstName).ToListAsync();
            }
            else if (LastName != null && !Ascending)
            {
                mailingContacts = await _context.MailingContacts.Where(c => c.LastName == LastName).OrderByDescending(c => c.LastName).ThenBy(c => c.FirstName).ToListAsync();
            }
            else if (!Ascending)
            {
                mailingContacts = await _context.MailingContacts.OrderByDescending(c => c.LastName).ThenBy(c => c.FirstName).ToListAsync();
            }
            else
            {
                mailingContacts = await _context.MailingContacts.OrderBy(c => c.LastName).ThenBy(c => c.FirstName).ToListAsync();
            }

            
            return mailingContacts;
        }

        // GET: api/MailingContacts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MailingContact>> GetMailingContact(long id)
        {
            var mailingContact = await _context.MailingContacts.FindAsync(id);

            if (mailingContact == null)
            {
                return NotFound();
            }

            return mailingContact;
        }

        // PUT: api/MailingContacts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMailingContact(long id, MailingContact mailingContact)
        {
            if (id != mailingContact.Id)
            {
                return BadRequest();
            }

            _context.Entry(mailingContact).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MailingContactExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/MailingContacts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MailingContact>> PostMailingContact(MailingContact mailingContact)
        {
            _context.MailingContacts.Add(mailingContact);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMailingContact", new { id = mailingContact.Id }, mailingContact);
        }

        // DELETE: api/MailingContacts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMailingContact(long id)
        {
            var mailingContact = await _context.MailingContacts.FindAsync(id);
            if (mailingContact == null)
            {
                return NotFound();
            }

            _context.MailingContacts.Remove(mailingContact);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MailingContactExists(long id)
        {
            return _context.MailingContacts.Any(e => e.Id == id);
        }
    }
}
