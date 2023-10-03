using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ToDoList_PG.Entities
{
    public class Tarefa
    {
        public Tarefa()
        {

            Task_Concluded = false;
            IsDeleted = false;

        }

        public Guid Id { get; set; }
        public string Task_Title { get; set; }
        public string Task_Description { get; set; }
        public DateTime Start_Date { get; set; }
        public DateTime End_Date { get; set; }
        public bool Task_Concluded { get; set; }
        public bool IsDeleted { get; set; }

        public void Update(string title, string description, DateTime start_date, DateTime end_date)
        {
            Task_Title = title;
            Task_Description = description;
            Start_Date = start_date;
            End_Date = end_date;

        }
        public void ItsConcluded()
        {
            Task_Concluded = true;
        }
        public void Delete()
        {
            IsDeleted = true;
        }
    }
}

