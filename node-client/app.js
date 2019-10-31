const todoService = require('./get-todo-service')
const minimist = require('minimist');

var args = minimist(process.argv.slice(2));

// appeler 'node app.js -a -c "contenu du todo" -n "nom du todo" par exemple

if (args.c)
    args.content = args.c;
if (args.n)
    args.name = args.n;

if (args.h) {
    console.log('sans argument, l\'application retourne tous les todo du serveur\r\n');
    console.log('avec l\'argument -a, l\'application ajoute un todo de nom -n et de contenu -c');
    console.log('exemple: \'node .\\app.js -a -n "Mon nom" -c "Mon contenu"'); 
}
else if (args.a) {

    todoService.AddTodo(args, (err, resp) => {
        todoService.GetAllTodo({}, (error, res) => {
            console.log(res)
        });
    });
}
else {
    todoService.GetAllTodo({}, (error, res) => {
        console.log(res)
    });
}
