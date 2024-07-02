using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Kafka_for_web.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Kafka_for_web.DataAccess;
using Kafka_for_web.Models;

namespace Kafka_for_web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProducerController : ControllerBase
    {
        private readonly KafkaContext _context;

        public ProducerController(KafkaContext context)
        {
            _context = context;
        }

        // GET: api/Producer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producer>>> GetProducers()
        {
            return await _context.Producers.ToListAsync();
        }

        // GET: api/Producer/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Producer>> GetProducer(long id)
        {
            var producer = await _context.Producers.FindAsync(id);

            if (producer == null)
            {
                return NotFound();
            }

            return producer;
        }

        // PUT: api/Producer/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducer(long id, Producer producer)
        {
            if (id != producer.Id)
            {
                return BadRequest();
            }

            _context.Entry(producer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProducerExists(id))
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

        // POST: api/Producer
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Producer>> PostProducer(Producer producer)
        {
            _context.Producers.Add(producer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProducer", new { id = producer.Id }, producer);
        }
        
        // * Post a new message
        [HttpPost("/message")]
        // Takes parameter producerID. 
        public async Task<IActionResult> PostMessage(Message message)
        {
            var producer = await _context.Producers.FindAsync(message.ProducerId);

            foreach (var p in await _context.Producers.ToListAsync())
            {
                Console.WriteLine("Producer id: " + p.Id);
            }
            
            if (producer == null)
            {
                return NotFound();
            }
            // producer.Messages ??= new List<Message>();
            
            // dont save the changes to the database; Messages don't belong in the database; 
            // producer.Messages.Add(message);
            // await _context.SaveChangesAsync();
            
            // if that is successful, each message should be written to the log file;  
            var logPath = @"logs/users/log1.txt";  
            
            // Will write to a text file
            Logger.Write(logPath, message.Value);
            
            return CreatedAtAction("GetProducer", new { id = producer.Id }, producer);
        }
        
        // DELETE: api/Producer/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducer(long id)
        {
            var producer = await _context.Producers.FindAsync(id);
            if (producer == null)
            {
                return NotFound();
            }

            _context.Producers.Remove(producer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProducerExists(long id)
        {
            return _context.Producers.Any(e => e.Id == id);
        }
    }
}
