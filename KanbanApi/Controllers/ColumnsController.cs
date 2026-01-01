using KanbanApi.Data;
using KanbanApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KanbanApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColumnsController : ControllerBase
    {
        private readonly AppDbContext _context;

        // Constructor එකෙන් Database Connection එක ඉල්ලගන්නවා (Dependency Injection)
        public ColumnsController(AppDbContext context)
        {
            _context = context;
        }

        // 1. GET: ඔක්කොම Columns සහ Cards ටික ගේන්න
        [HttpGet]
        public async Task<ActionResult<IEnumerable<KanbanColumn>>> GetColumns()
        {
            // Database එකට ගිහින් Columns අරන්, ඒවට අදාළ Cards (Include) එක්කම ගේන්න
            return await _context.Columns.Include(c => c.Cards).ToListAsync();
        }

        // 2. POST: අලුත් Column එකක් හදන්න (උදා: "Doing")
        [HttpPost]
        public async Task<ActionResult<KanbanColumn>> CreateColumn(KanbanColumn column)
        {
            _context.Columns.Add(column);
            await _context.SaveChangesAsync(); // Save බට්න් එක ඔබනවා වගේ
            return CreatedAtAction(nameof(GetColumns), new { id = column.Id }, column);
        }
    }
}