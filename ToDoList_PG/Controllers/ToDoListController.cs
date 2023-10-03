using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList_PG.Controllers;
using ToDoList_PG.Entities;
using ToDoList_PG.Persistence;
using ToDoList_PG.Mappers;
using ToDoList_PG.Models;

namespace ToDoList_PG.Controllers
{
    [Route("api/todolist")]
    [ApiController]
    public class ToDoListController : ControllerBase
    {
        private readonly ToDoListDbContext Context;
        private readonly IMapper Mapper;

        public ToDoListController(ToDoListDbContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }
        /// <summary>
        /// Obter todas as tarefas ordenadas por data de vencimento.
        /// </summary>
        /// <returns>Tarefas diárias</returns>
        /// <response code="200"> Sucesso! </response>
        [HttpGet("GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            var tarefa = Context.Tarefas.Where(t=>!t.IsDeleted).OrderBy(t => t.End_Date).ToList();

            var viewModel = Mapper.Map<List<TarefaViewModel>>(tarefa);

            return Ok(viewModel);
    
        }
        /// <summary>
        /// Obter Todas as tarefas concluídas de forma ordenada.
        /// </summary>
        /// <returns>Tarefas Concluídas:</returns>
        /// <response code="200"> Sucesso! </response>
        [HttpGet("GetAllConcludedTasks")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAllConcludedTasks()
        {
            var tarefa = Context.Tarefas.OrderBy(t => t.End_Date).Where(t => !t.IsDeleted).Where(t=>t.Task_Concluded).ToList();

            var viewModel = Mapper.Map<List<TarefaViewModel>>(tarefa);
            return Ok(viewModel);
        }
        /// <summary>
        /// Obter todas as tarefas vencidas.
        /// </summary>
        /// <returns>Tarefas Vencidas:</returns>
        /// <response code="200"> Sucesso! </response>
        [HttpGet("GetAllExpiredTasks")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAllExpiredTasks()
        {
            var tarefa = Context.Tarefas.OrderBy(t => t.End_Date).Where(t => !t.IsDeleted).Where(t => t.End_Date < DateTime.Now).ToList();

            var viewModel = Mapper.Map<List<TarefaViewModel>>(tarefa);
            return Ok(viewModel);
        }

        /// <summary>
        /// Obter a tarefa digitando o seu título.
        /// </summary>
        /// <param name="title">Título da Tarefa</param>
        /// <returns>Dados da tarefa:</returns>
        /// <response code="200"> Sucesso! </response>
        /// <response code="404"> Tarefa não encontrada.</response>
        [HttpGet("GetByTitle")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetByTitle(string title)
        {
            var tarefa = Context.Tarefas.SingleOrDefault(t => t.Task_Title == title);
            if (tarefa == null)
            {
                return NotFound();
            }

            var viewModel = Mapper.Map<TarefaViewModel>(tarefa);
            return Ok(viewModel);
        }

        /// <summary>
        /// Cadastrar uma Tarefa
        /// </summary>
        /// <param name="input">Cadastro de dados:</param>
        /// <returns>Objeto recém-criado:</returns>
        /// <response code="201"> Tarefa Cadastrada.</response>
        /// /// <response code="500"> ERRO: Dados inválidos. </response>
        [HttpPost("Post")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post(TarefaInputModel input)
        {   
            var tarefa = Mapper.Map<Tarefa>(input);
            Context.Tarefas.Add(tarefa);
            Context.SaveChanges();
            return CreatedAtAction(nameof(GetByTitle), new { title = tarefa.Task_Title}, tarefa);
        }

        /// <summary>
        /// Definir uma tarefa como concluída.
        /// </summary>
        /// <param name="title">Título da Tarefa</param>
        /// <returns> </returns>
        /// <response code="404"> Tarefa não encontrada. </response>
        /// <response code="204"> Tarefa atualizada para concluída. </response>
        [HttpPut("TaskConcluded")]
        public IActionResult TaskConcluded_(string title)
        {
            var tarefa = Context.Tarefas.SingleOrDefault(t => t.Task_Title == title);

            if (tarefa == null)
            {
                return NotFound();
            }

            tarefa.ItsConcluded();
            Context.SaveChanges();
            return NoContent();

        }

        /// <summary>
        /// Atualizar os dados de certa Tarefa
        /// </summary>
        /// <param name="input">Dados da Tarefa</param>
        /// <param name="title">Título da Tarefa</param>
        /// <returns> </returns>
        /// <response code="404"> Tarefa não encontrada. </response>
        /// <response code="204"> Dados atualizados. </response>
        [HttpPut("Update/{title}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Update(TarefaInputModel input, string title)
        {
            var tarefa = Context.Tarefas.SingleOrDefault(t => t.Task_Title == title);

            if (tarefa == null)
            {
                return NotFound();
            }

            tarefa.Update(input.Task_Title, input.Task_Description, input.Start_Date, input.End_Date);
            Context.Tarefas.Update(tarefa);
            Context.SaveChanges();
            return NoContent();
        }

        /// <summary>
        /// Deletar uma Tarefa
        /// </summary>
        /// <param name="title">Título da Tarefa</param>
        /// <returns> </returns>
        /// <response code="404"> Tarefa não encontrada. </response>
        /// <response code="204"> Dados deletados. </response>
        [HttpDelete("Delete/{title}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Delete(string title)
        {
            var tarefa = Context.Tarefas.SingleOrDefault(t => t.Task_Title == title);

            if (tarefa == null)
            {
                return NotFound();
            }

            tarefa.Delete();
            Context.SaveChanges();
            return NoContent();

        }


    }
}
