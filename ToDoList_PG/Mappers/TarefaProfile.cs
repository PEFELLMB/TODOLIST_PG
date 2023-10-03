using AutoMapper;
using ToDoList_PG.Entities;
using ToDoList_PG.Models;
using ToDoList_PG.Persistence;

namespace ToDoList_PG.Mappers
{
    public class TarefaProfile: Profile
    {
        public TarefaProfile()
        {
            CreateMap<Tarefa, TarefaViewModel>();

            CreateMap<TarefaInputModel, Tarefa>();
        }
    }
}
