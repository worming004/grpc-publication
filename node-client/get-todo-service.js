// Chemin d'accès au fichier proto
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
// Pour faciliter notre démo, nous n'utiliserons pas de crédential.
const todoService = new api.TodoService('localhost:5001', grpc.credentials.createInsecure());

module.exports = todoService;