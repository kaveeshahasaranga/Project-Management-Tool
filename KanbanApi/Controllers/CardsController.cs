using KanbanApi.Data;
using KanbanApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KanbanApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CardsController(AppDbContext context)
        {
            _context = context;
        }

        // 1. GET: ඔක්කොම කාඩ් ටික බලන්න
        [HttpGet]
        public async Task<ActionResult<IEnumerable<KanbanCard>>> GetCards()
        {
            return await _context.Cards.ToListAsync();
        }

        // 2. POST: අලුත් කාඩ් එකක් හදන්න
        // මෙතනදි අපි ColumnId එක අනිවාර්යයෙන් එවන්න ඕන
        [HttpPost]
        public async Task<ActionResult<KanbanCard>> CreateCard(KanbanCard card)
        {
            // මුලින්ම බලනවා එවන ColumnId එක ඇත්තටම තියෙනවද කියලා
            var column = await _context.Columns.FindAsync(card.ColumnId);
            if (column == null)
            {
                return BadRequest("වැරදි Column ID එකක්! තාත්තා කෙනෙක් නැතුව ළමයි හදන්න බෑ.");
            }

            _context.Cards.Add(card);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCards), new { id = card.Id }, card);
        }

        // 3. PUT: කාඩ් එකක් වෙනස් කරන්න (Move කරන්න Drag & Drop වලට ඕන වෙන්නේ මේක)
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCard(int id, KanbanCard card)
        {
            if (id != card.Id) return BadRequest();

            _context.Entry(card).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Cards.Any(e => e.Id == id)) return NotFound();
                else throw;
            }

            return NoContent();
        }
    }
}