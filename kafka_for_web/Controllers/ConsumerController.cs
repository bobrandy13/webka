using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Kafka_for_web.DataAccess;
using Kafka_for_web.Models;
using System.Collections;
using System.Net;

namespace Kafka_for_web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsumerController : ControllerBase
    {
        private readonly KafkaContext _context;

        public ConsumerController(KafkaContext context)
        {
            _context = context;
        }

        // GET: api/Consumer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Consumer>>> GetConsumers()
        {
            return await _context.Consumers.ToListAsync();
        }

        // GET: api/Consumer/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Consumer>> GetConsumer(long id)
        {
            var consumer = await _context.Consumers.FindAsync(id);

            if (consumer == null)
            {
                return NotFound();
            }

            return consumer;
        }

        // PUT: api/Consumer/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConsumer(long id, Consumer consumer)
        {
            if (id != consumer.Id)
            {
                return BadRequest();
            }

            _context.Entry(consumer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConsumerExists(id))
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

        [HttpGet("subscribe")]
        public async Task<IActionResult> SubscribeToTopic(string consumerName, string clusterName, string topicName, ConsumerOptionalParams optionalParams, CancellationToken cancellationToken, MessageOffset offset = MessageOffset.latest)
        {
            // this should estsablish a long polling connection to the client 
            await Task.Delay(500, cancellationToken);

            var logPath = $"logs/{clusterName}/{topicName}/log.txt";

            var message = Logger.FetchMessage(logPath, offset);

            // TODO: increase the offset of the current consumer and then save it in the metadata

            return new ObjectResult(new { Message = message ?? "Long polling timeout!" });
        }

        /// <summary>
        /// Method <c>fetchMessage()</c> checks for new messages depending on the offset
        /// </summary>

        // POST: api/Consumer
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Consumer>> PostConsumer(Consumer consumer)
        {
            _context.Consumers.Add(consumer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetConsumer", new { id = consumer.Id }, consumer);
        }

        // DELETE: api/Consumer/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConsumer(long id)
        {
            var consumer = await _context.Consumers.FindAsync(id);
            if (consumer == null)
            {
                return NotFound();
            }

            _context.Consumers.Remove(consumer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ConsumerExists(long id)
        {
            return _context.Consumers.Any(e => e.Id == id);
        }
    }
}
