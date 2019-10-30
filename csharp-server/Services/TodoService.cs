using System.Threading.Tasks;
using System.IO;
using Grpc.Core;
using System.Linq;
using System.Text;
using TodoApi;

namespace csharp_server
{
    public class TodoService : TodoApi.TodoService.TodoServiceBase
    {
        const string FOLDER_NAME = "Todo";
        public override Task<AddTodoResponse> AddTodo(AddTodoRequest request, ServerCallContext context)
        {
            File.WriteAllLines(BuildFilePath(request.Name), new[] { request.Content });
            return Task.FromResult(new AddTodoResponse());
        }

        public override Task<GetAllTodoResponse> GetAllTodo(GetAllTodoRequest request, ServerCallContext context)
        {
            var result = new GetAllTodoResponse();
            result.Todo.Add(GetAllTodo());

            return Task.FromResult(result);
        }

        private Todo[] GetAllTodo()
        {
            var allFileNames = Directory.GetFiles(FOLDER_NAME);
            return allFileNames.Select(fName =>
            {
                return new Todo
                {
                    Name = fName,
                    Content = GetFileContent(fName)
                };
            }).ToArray();
        }

        private string BuildFilePath(string fileName)
        {
            return new StringBuilder()
                        .Append(FOLDER_NAME)
                        .Append('\\')
                        .Append(fileName)
                        .ToString();
        }

        private string GetFileContent(string todoName)
        {
            return File.ReadAllText(BuildFilePath(todoName));
        }
    }
}
