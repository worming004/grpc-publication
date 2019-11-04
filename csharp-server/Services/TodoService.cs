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
        const string FOLDER_NAME = "todo";
        const string EXTENSION = ".td";
        public override Task<AddTodoResponse> AddTodo(AddTodoRequest request, ServerCallContext context)
        {
            var fileName = BuildFilePath(request.Name);
            using (var fs = new FileStream(fileName, FileMode.Create))
            using (var writer = new StreamWriter(fs))
            {
                writer.Write(request.Content);
                writer.Close();
            }

            //File.WriteAllLines(BuildFilePath(request.Name), new[] { request.Content });
            return Task.FromResult(new AddTodoResponse());
        }

        public override Task<GetAllTodoResponse> GetAllTodo(Google.Protobuf.WellKnownTypes.Empty request, ServerCallContext context)
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
                    Name = Path.GetFileNameWithoutExtension(fName),
                    Content = GetFileContent(fName)
                };
            }).ToArray();
        }

        private string BuildFilePath(string fileName)
        {
            return Path.Join(FOLDER_NAME, fileName + EXTENSION);
        }

        private string GetFileContent(string todoName)
        {
            return File.ReadAllText(todoName);
        }
    }
}
