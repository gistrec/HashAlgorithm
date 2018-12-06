const exec = require('child_process').exec;

const express = require('express');
const app = express();

app.use(require('express-fileupload')());
app.use(express.static('.'));
app.use(require("body-parser").urlencoded({extended : true}));

app.post('/file_hash', function(req, res) {
	let file = req.files['0']
	console.log(req.files['0'])
  	console.log('Upload file: ' + file.name);
	file.mv(file.name, function(err) {
		exec('Cryptography.exe file_hash ' + file.name, function(err, stdout, stderr) {
    		res.send(stdout);
  		});
  	});
});

app.post('/string_hash', function(req, res) {
	exec('Cryptography.exe string_hash ' + req.body.key + ' ' + req.body.message, function(err, stdout, stderr) {
		res.send(stdout);
	});
})

app.get('*', function(req, res) {
	console.log('GET: index.html');
	res.sendFile(__dirname + '/index.html');
})

app.listen(8080);
console.log('Server listen 8080 port')