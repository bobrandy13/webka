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
    public class PartitionController : ControllerBase
    {
        private readonly KafkaContext _context;

        public PartitionController(KafkaContext context)
        {
            _context = context;
        }

        // GET: api/Partition
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Partition>>> GetPartitions()
        {
            return await _context.Partitions.ToListAsync();
        }

        // GET: api/Partition/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Partition>> GetPartition(long id)
        {
            var partition = await _context.Partitions.FindAsync(id);

            if (partition == null)
            {
                return NotFound();
            }

            return partition;
        }

        // PUT: api/Partition/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPartition(long id, Partition partition)
        {
            if (id != partition.Id)
            {
                return BadRequest();
            }

            _context.Entry(partition).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PartitionExists(id))
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

        // POST: api/Partition
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Partition>> PostPartition(Partition partition)
        {
            _context.Partitions.Add(partition);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPartition", new { id = partition.Id }, partition);
        }

        // DELETE: api/Partition/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePartition(long id)
        {
            var partition = await _context.Partitions.FindAsync(id);
            if (partition == null)
            {
                return NotFound();
            }

            _context.Partitions.Remove(partition);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PartitionExists(long id)
        {
            return _context.Partitions.Any(e => e.Id == id);
        }
    }
}
