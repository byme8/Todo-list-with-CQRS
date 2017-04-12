using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Data;

namespace Todo.Services
{
	public class TodoService
	{
		private List<TodoTask> Tasks;

		public TodoService()
		{
			this.Tasks = new List<TodoTask>();
			for (int i = 0; i < 10; i++)
			{
				this.Tasks.Add(new TodoTask
				{
					Key = Guid.NewGuid(),
					Text = "Test" + i,
					Finished = i % 2 == 0
				});
			}
		}

		public void Remove(Guid key)
		{
			this.Tasks.RemoveAll(o => o.Key == key);
		}

		public IEnumerable<TodoTask> GetAll()
		{
			return this.Tasks;
		}

		public void Toggle(Guid key)
		{
			var task = this.Tasks.FirstOrDefault(o => o.Key == key) 
				?? throw new InvalidOperationException($"Task with key {key} does not exist");

			task.Finished = !task.Finished;
		}

		public void New(string message)
		{
			this.Tasks.Add(new TodoTask
			{
				Key = Guid.NewGuid(),
				Text = message,
				Finished = false
			});
		}
	}
}
