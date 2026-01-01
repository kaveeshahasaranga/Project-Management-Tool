namespace KanbanApi.Models
{
    public class KanbanCard
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Order { get; set; } // පෝලිමේ කීවෙනියාද කියන එක (Drag & Drop වලට ඕන)

        // ළමයෙක්ට අනිවාර්යයෙන් තාත්තා කෙනෙක් (Column එකක්) ඉන්න ඕන
        public int ColumnId { get; set; } 
    }
}