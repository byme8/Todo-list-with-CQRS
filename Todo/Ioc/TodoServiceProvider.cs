using CQRS.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Todo.Commands;
using Todo.CQRS;
using Todo.Queries;
using Todo.Services;

namespace Todo.Ioc
{
	public static class TodoServiceProvider
    {
		public static void AddTodoService(this IServiceCollection services)
		{
			services.AddSingleton<IQueryManager, TodoQueryManager>();
			services.AddSingleton<IQueryStorage, TodoQueryStorage>();
			services.AddSingleton<RemoveTaskHandler>();
			services.AddSingleton<AddNewTaskHandler>();
			services.AddSingleton<ToggleHandler>();
			services.AddSingleton<AllTodoTasksQueryHandler>();
			services.AddSingleton<TodoService>();
		}
    }
}
