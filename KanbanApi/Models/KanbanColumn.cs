namespace KanbanApi.Models
{
    public class KanbanColumn
    {
        public int Id { get; set; } // හැඳුනුම්පත (ID)
        public string Title { get; set; } = string.Empty; // නම (To Do / Done)
        
        // තාත්තා කෙනෙක්ට ළමයි ගොඩක් ඉන්න පුළුවන් (One-to-Many)
        public List<KanbanCard> Cards { get; set; } = new List<KanbanCard>(); 
    }
}