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

        // POST: api/cards (කාඩ් එකක් අලුතින් හදන තැන)
        [HttpPost]
        public async Task<ActionResult<KanbanCard>> CreateCard(KanbanCard card)
        {
            // 1. කාඩ් එක Database එකට එකතු කරනවා
            _context.Cards.Add(card);
            
            // 2. වෙනස්කම් Save කරනවා
            await _context.SaveChangesAsync();

            // 3. හරි ගියාම Frontend එකට "හරි" කියලා පණිවිඩයක් යවනවා
            return Ok(card);
        }
    }
}