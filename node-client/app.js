const todoService = require('./get-todo-service')
const minimist = require('minimist');

var args = minimist(process.argv.slice(2));

// appeler 'node app.js -c "contenu du todo" -n "nom du todo" par exemple

if (args.c)
    args.content = args.c;
if (args.n)
    args.name = args.n;

todoService.AddTodo(args, (err, resp) => {
    console.log('ok');
    console.log(err);
    console.log(resp);
    todoService.GetAllTodo({}, (error, res) => {
        console.log(res)
    }
    );
});
