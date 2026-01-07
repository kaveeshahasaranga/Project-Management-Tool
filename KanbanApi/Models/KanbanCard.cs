namespace KanbanApi.Models
{
    public class KanbanCard
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        
        // මේක හරිම වැදගත්! කාඩ් එක අයිති මොන Column එකටද කියලා කියන්නේ මෙයා.
        public int ColumnId { get; set; } 
    }
}