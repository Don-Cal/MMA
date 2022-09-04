using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MMA.API.Data;
using MMA.API.Data.Entities;
using MMA.API.Models;

namespace MMA.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [Authorize]

  public class ContactsController : ControllerBase
  {
    private readonly MMADbContext _context;
    private readonly IMapper mapper;

    public ContactsController(MMADbContext context, IMapper mapper)
    {
      _context = context;
      this.mapper = mapper;
    }

    // GET: api/Contacts
    [HttpGet]
    public async Task<ActionResult<IEnumerable<dtoContactOnly>>> GetContacts()
    {
      if (_context.Contacts == null)
      {
        return NotFound();
      }

      var dtoContacts = await _context.Contacts
        .Include(q => q.Client)
        .ProjectTo<dtoContactOnly>(mapper.ConfigurationProvider)
        .ToListAsync();

      return Ok(dtoContacts);
    }

    // GET: api/Contacts/5
    [HttpGet("{id}")]
    public async Task<ActionResult<dtoContactOnly>> GetContact(int id)
    {
      var contact = await _context.Contacts
        .Include(q => q.Client)
        .ProjectTo<dtoContactOnly>(mapper.ConfigurationProvider)
        .FirstOrDefaultAsync(q => q.Id == id);

      if (contact == null)
      {
        return NotFound();
      }

      return contact;
    }

    // PUT: api/Contacts/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> PutContact(int id, dtoContactUpdate updatecontact)
    {
      if (id != updatecontact.Id)
      {
        return BadRequest();
      }

      var contact = await _context.Contacts.FindAsync(id);

      if(contact == null)
      {
        return NotFound();
      }

      mapper.Map(updatecontact, contact);
      _context.Entry(contact).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (! await ContactExists(id))
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

    // POST: api/Contacts
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<dtoContactCreate>> PostContact(dtoContactCreate newcontact)
    {
      var contact = mapper.Map<Contact>(newcontact);
      _context.Contacts.Add(contact);
      await _context.SaveChangesAsync();

      return CreatedAtAction(nameof(GetContact), new { id = contact.Id }, contact);
    }

    // DELETE: api/Contacts/5
    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> DeleteContact(int id)
    {
      var contact = await _context.Contacts.FindAsync(id);
      if (contact == null)
      {
        return NotFound();
      }

      _context.Contacts.Remove(contact);
      await _context.SaveChangesAsync();

      return NoContent();
    }

    private async Task<bool> ContactExists(int id)
    {
      return await _context.Contacts.AnyAsync(e => e.Id == id);
    }
  }
}
