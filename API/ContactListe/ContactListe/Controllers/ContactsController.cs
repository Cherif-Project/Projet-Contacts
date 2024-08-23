using ContactListe.Data;
using ContactListe.Models.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactListe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly ContatsDbContext _contatsDbContext;
        public ContactsController(ContatsDbContext contatsDbContext)
        {
            _contatsDbContext = contatsDbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contact>>> GetContact()
        {
            return await _contatsDbContext.Contacts.ToListAsync();
        }

        [HttpGet]
        [Route("{id:Guid}")] 
        public async Task<ActionResult<Contact>> GetContactById(Guid id) {
        
           var contatc = await _contatsDbContext.Contacts.Where(c => c.Id.Equals(id)).FirstOrDefaultAsync();
            if (contatc == null) { 
                return NoContent();
            }
            return contatc;
        }


        [HttpPost]
        public async Task<ActionResult<Contact>> CreateContact(Contact contact)
        {
            if (contact == null)
            {
                return NoContent();
            }
            _contatsDbContext.Contacts.AddAsync(contact);
            await _contatsDbContext.SaveChangesAsync();
            return Ok(contact);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> ModifierContact(Guid id, Contact contact)
        {
            if (!id.Equals(contact.Id)) {
                return BadRequest("les identifiant sont différent");
            }
            var contactToUpdate = await _contatsDbContext.Contacts.FindAsync(id);
            if (contactToUpdate == null) { 
            return NotFound($"cous whith id = {id} not found");
            }
            map(contactToUpdate,contact);
            await _contatsDbContext.SaveChangesAsync();
            return NoContent();

        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteContact(Guid id)
        {
            var contact = await _contatsDbContext.Contacts.FindAsync(id);
            if (contact == null) { 
            return NotFound();
            }
            _contatsDbContext.Contacts.Remove(contact) ;
            await _contatsDbContext.SaveChangesAsync();
            return NoContent();
        }

        private void map(Contact contactToUpdate, Contact contact)
        {
             contactToUpdate.Name = contact.Name;
            contactToUpdate.Email = contact.Email;
            contactToUpdate.Phone = contact.Phone;
        }
    }
}
