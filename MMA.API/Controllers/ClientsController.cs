using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MMA.API.Data;
using MMA.API.Data.Entities;
using MMA.API.Models;
using MMA.API.Static;
using AutoMapper;
using System.Collections;
using Microsoft.AspNetCore.Authorization;
using AutoMapper.QueryableExtensions;

namespace MMA.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [Authorize]
  public class ClientsController : ControllerBase
  {
    private readonly MMADbContext _context;
    private readonly IMapper mapper;
    private readonly ILogger<ClientsController> logger;

    public ClientsController(MMADbContext context, IMapper mapper, ILogger<ClientsController> logger)
    {
      _context = context;
      this.mapper = mapper;
      this.logger = logger;
    }

    // GET: api/Clients
    [HttpGet]
    public async Task<ActionResult<IEnumerable<dtoClientOnly>>> GetClients()
    {
      try
      {
        var clients = await _context.Clients.ToListAsync();
        var dtoClients = mapper.Map<IEnumerable<dtoClientOnly>>(clients);

        return Ok(dtoClients);
      }
      catch (Exception ex)
      {
        logger.LogError(ex, $"Error Performing GET in {nameof(GetClients)}");
        return StatusCode(500, Messages.Error500Message);
      }
    }

    // GET: api/Clients/5
    [HttpGet("{id}")]
    public async Task<ActionResult<dtoClient>> GetClient(int id)
    {
      try
      {
        //        var client = await _context.Clients.FindAsync(id);
        var client = await _context.Clients
          .Include(c => c.Contacts)
          .ProjectTo<dtoClient>(mapper.ConfigurationProvider)
          .FirstOrDefaultAsync(q => q.Id ==id); 

        if (client == null)
        {
          logger.LogWarning($"Record Not Found: {nameof(GetClient)} - ID: {id}");
          return NotFound();
        }
        var dtoClient = mapper.Map<dtoClient>(client);

        return Ok(dtoClient);
      }
      catch (Exception ex)
      {
        logger.LogError(ex, $"Error Performing GET in {nameof(GetClient)}");
        return StatusCode(500, Messages.Error500Message);
      }

    }

    // PUT: api/Clients/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> PutClient(int id, dtoClientUpdate dtoClient)
    {
      logger.LogInformation($"Update  {nameof(PutClient)} - ID: {id}");

      if (id != dtoClient.Id)
      {
        logger.LogWarning($"Update ID invalid in {nameof(PutClient)} - ID: {id}");
        return BadRequest();
      }

      var client = await _context.Clients.FindAsync(id);

      if (client == null)
      {
        logger.LogWarning($"{nameof(entityClient)} record not found in {nameof(PutClient)} - ID: {id}");
        return NotFound();
      }

      mapper.Map(dtoClient, client);
      _context.Entry(client).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException ex)
      {
        if (!await ClientExists(id))
        {
          return NotFound();
        }
        else
        {
          logger.LogError(ex, $"Error Performing GET in {nameof(PutClient)}");
          return StatusCode(500, Messages.Error500Message);
        }
      }

      return NoContent();
    }

    // POST: api/Clients
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<dtoClientCreate>> PostClient(dtoClientCreate dtoClient)
    {
      try
      {
        var client = mapper.Map<entityClient>(dtoClient);
        await _context.Clients.AddAsync(client);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetClient), new { id = client.Id }, client);
      }
      catch (Exception ex)
      {
        logger.LogError(ex, $"Error Performing POST in {nameof(PostClient)}", dtoClient);
        return StatusCode(500, Messages.Error500Message);
      }

    }

    // DELETE: api/Clients/5
    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> DeleteClient(int id)
    {
      try
      {
        var client = await _context.Clients.FindAsync(id);
        if (client == null)
        {
          logger.LogWarning($"{nameof(entityClient)} record not found in {nameof(DeleteClient)} - ID: {id}");
          return NotFound();
        }

        _context.Clients.Remove(client);
        await _context.SaveChangesAsync();

        return NoContent();
      }
      catch (Exception ex)
      {
        logger.LogError(ex, $"Error Performing DELETE in {nameof(DeleteClient)}");
        return StatusCode(500, Messages.Error500Message);
      }
    }

    private async Task<bool> ClientExists(int id)
    {
      return await _context.Clients.AnyAsync(e => e.Id == id);
    }

  }
}
