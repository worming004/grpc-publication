using System.Threading.Tasks;
using System.IO;
using Grpc.Core;
using System.Linq;
using System.Text;

namespace csharp_server
{
    public class TodoService : Todo.TodoService.TodoServiceBase
    {
        const string FOLDER_NAME = "Todo";
        public override Task<Todo.AddTodoResponse> AddTodo(Todo.AddTodoRequest request, ServerCallContext context)
        {
            File.WriteAllLines(BuildFilePath(request.Name), new[] { request.Content });
            return Task.FromResult(new Todo.AddTodoResponse());
        }

        public override Task<Todo.GetAllTodoResponse> GetAllTodo(Todo.GetAllTodoRequest request, ServerCallContext context) {
            // var description = GetFileContent(requ)
        }

        private Todo.SingleTodo[] GetAllTodo() {
            var allFileNames = Directory.GetFiles(FOLDER_NAME);
            return allFileNames.Select(fName => {
                return new Todo.SingleTodo{
                    Name = fName,
                    Content = GetFileContent(fName)
                };
            }).ToArray();
        }

        private string BuildFilePath(string fileName) {
            return new StringBuilder()
                        .Append(FOLDER_NAME)
                        .Append('\\')
                        .Append(fileName)
                        .ToString();
        }

        private string GetFileContent(string todoName) {
            return File.ReadAllText(BuildFilePath(todoName));
        }
    }
}
