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
            var path = $"{System.IO.Directory.GetCurrentDirectory()}/logs/{cluster.Name}";
            try
            {
                if (Directory.Exists(path))
                {
                    Console.WriteLine("This directory already exists, and therefore so does this cluster");
                    return NotFound();
                }
                _context.Clusters.Add(cluster);
                await _context.SaveChangesAsync();

                var di = Directory.CreateDirectory(path);
                Console.WriteLine("The directory was created successfully at {0}, at path {1}", Directory.GetCreationTime(path), path);

                return CreatedAtAction("GetCluster", new { id = cluster.Id }, cluster);
            }
            catch (Exception e)
            {
                Console.WriteLine("This process failed {0}", e);
            }

            return NotFound();
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
