using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamsApi.Models;
using TeamsApi.Controllers;

namespace TeamsApi.Controllers
{
    [Route("api/player")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly PlayerContext _context;
        private readonly TeamContext _teamContext;

        public PlayerController(PlayerContext context, TeamContext teamContext)
        {
            _context = context;
            _teamContext = teamContext;
        }

        // GET: api/Player
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Player>>> GetPlayers()
        {
            return await _context.Players.ToListAsync();
        }

        // GET: api/Player/lastname/Smith
        [HttpGet("lastname/{lastname}")]
        public async Task<ActionResult<IEnumerable<Player>>> GetPlayersByLastName(string lastname)
        {
            List<Player> playerList = new List<Player>();
            playerList = await _context.Players.ToListAsync();

            IEnumerable<Player> playersByLastName = 
                from p in playerList
                where p.LastName == lastname
                select p;

            if (playersByLastName == null) 
            {
                return NotFound();
            }

            return playersByLastName.ToList();
                
        }
        // GET: api/Player/team/2
        [HttpGet("team/{id}")]
        public async Task<ActionResult<IEnumerable<Player>>> GetTeamRoster(long id)
        {
            List<Player> playerList = new List<Player>();
            playerList = await _context.Players.ToListAsync();

            IEnumerable<Player> teamRoster = 
                from p in playerList
                where p.TeamId == id
                select p;
            
            if (teamRoster == null) return NotFound();

            return teamRoster.ToList().OrderBy(p => p.LastName).ToList();
        }

        // GET: api/Player/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Player>> GetPlayer(long id)
        {
            var player = await _context.Players.FindAsync(id);

            if (player == null)
            {
                return NotFound();
            }

            return player;
        }

        // PUT: api/Player/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlayer(long id, Player player)
        {
            if (id != player.Id)
            {
                return BadRequest();
            }

            _context.Entry(player).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerExists(id))
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

        // POST: api/Player
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Player>> PostPlayer(Player player)
        {
            if(_context.Players.Count(p => p.TeamId == player.TeamId) >= 8)
            {
                return BadRequest();
            }
         
            _context.Players.Add(player);
            await _context.SaveChangesAsync(); 
            return CreatedAtAction("GetPlayer", new { id = player.Id }, player);
                    
        }

        // DELETE: api/Player/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Player>> DeletePlayer(long id)
        {
            var player = await _context.Players.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }

            _context.Players.Remove(player);
            await _context.SaveChangesAsync();

            return player;
        }

        private bool PlayerExists(long id)
        {
            return _context.Players.Any(e => e.Id == id);
        }

        private bool PlayerExists(string lastName)
        {
            return _context.Players.Any(e => e.LastName == lastName);
        }
    }
}
