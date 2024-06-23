using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Kafka_for_web.DataAccess;
using Kafka_for_web.Models;

namespace Kafka_for_web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsumerGroupController : ControllerBase
    {
        private readonly KafkaContext _context;

        public ConsumerGroupController(KafkaContext context)
        {
            _context = context;
        }

        // GET: api/ConsumerGroup
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConsumerGroup>>> GetConsumerGroups()
        {
            return await _context.ConsumerGroups.ToListAsync();
        }

        // GET: api/ConsumerGroup/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ConsumerGroup>> GetConsumerGroup(long id)
        {
            var consumerGroup = await _context.ConsumerGroups.FindAsync(id);

            if (consumerGroup == null)
            {
                return NotFound();
            }

            return consumerGroup;
        }

        // PUT: api/ConsumerGroup/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConsumerGroup(long id, ConsumerGroup consumerGroup)
        {
            if (id != consumerGroup.Id)
            {
                return BadRequest();
            }

            _context.Entry(consumerGroup).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConsumerGroupExists(id))
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

        // POST: api/ConsumerGroup
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ConsumerGroup>> PostConsumerGroup(ConsumerGroup consumerGroup)
        {
            _context.ConsumerGroups.Add(consumerGroup);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetConsumerGroup", new { id = consumerGroup.Id }, consumerGroup);
        }

        // DELETE: api/ConsumerGroup/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConsumerGroup(long id)
        {
            var consumerGroup = await _context.ConsumerGroups.FindAsync(id);
            if (consumerGroup == null)
            {
                return NotFound();
            }

            _context.ConsumerGroups.Remove(consumerGroup);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ConsumerGroupExists(long id)
        {
            return _context.ConsumerGroups.Any(e => e.Id == id);
        }
    }
}
