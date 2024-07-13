using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Kafka_for_web.DataAccess;
using Kafka_for_web.Models;

namespace Kafka_for_web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClusterController : ControllerBase
    {
        private readonly KafkaContext _context;

        public ClusterController(KafkaContext context)
        {
            _context = context;
        }

        // GET: api/Cluster
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cluster>>> GetClusters()
        {
            Console.WriteLine(System.IO.Directory.GetCurrentDirectory());
            return await _context.Clusters.ToListAsync();
        }

        // GET: api/Cluster/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cluster>> GetCluster(long id)
        {
            var cluster = await _context.Clusters.FindAsync(id);

            if (cluster == null)
            {
                return NotFound();
            }

            return cluster;
        }

        // PUT: api/Cluster/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCluster(long id, Cluster cluster)
        {
            if (id != cluster.Id)
            {
                return BadRequest();
            }

            _context.Entry(cluster).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClusterExists(id))
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

        // POST: api/Cluster
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cluster>> PostCluster(Cluster cluster)
        {
            
            _context.Clusters.Add(cluster);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetCluster", new { id = cluster.Id }, cluster);
        }

        // DELETE: api/Cluster/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCluster(long id)
        {
            var cluster = await _context.Clusters.FindAsync(id);
            if (cluster == null)
            {
                return NotFound();
            }

            _context.Clusters.Remove(cluster);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClusterExists(long id)
        {
            return _context.Clusters.Any(e => e.Id == id);
        }
    }
}