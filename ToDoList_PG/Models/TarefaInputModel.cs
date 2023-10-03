namespace ToDoList_PG.Models
{
    public class TarefaInputModel
    {
        public string Task_Title { get; set; }
        public string Task_Description { get; set; }
        public DateTime Start_Date { get; set; }
        public DateTime End_Date { get; set; }
    }
}
