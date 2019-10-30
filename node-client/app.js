const PROTO_PATH = __dirname  + '/../proto/todoservice.proto';

const grpc = require('grpc');
const protoLoader = require('@grpc/proto-loader');

const packageDefinition = protoLoader.loadSync(
    PROTO_PATH,
    {
        keepCase: true,
        longs: String,
        enums: String,
        defaults: true,
        oneofs: true
    }
);

const api = grpc.loadPackageDefinition(packageDefinition).TodoApi;
const todoService = new api.TodoService('localhost:5001', grpc.credentials.createInsecure());

todoService.AddTodo({name: "first", content: "content"}, (err, resp) => {
    console.log('ok');
    console.log(err);
    console.log(resp);
    todoService.GetAllTodo({}, (error, res) => {
        console.log(res)}
        );
});
