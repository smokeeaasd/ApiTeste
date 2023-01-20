const { EventEmitter } = require("node:events");
const { Router } = require("express");
const express = require("express");
const bodyParser = require("body-parser");
const _router = Router();
const app = express();

app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: false }));

class API extends EventEmitter {
	/** @type {Router} */
	router;

	/** @type {String} */
	address;

	/** @type {Number} */
	port;

	/** @param {Number} port */
	constructor(port, address) {
		super();
		this.address = address;
		this.port = port;
		this.router = _router;
	}

	start() {
		this.emit("ready");
		_router.post("/send", (req, res) => {
			if (req.body.author && req.body.content) {
				res.status(201).send();
				res.end();

				console.log(`[${req.body.author}]: ${req.body.content}`);
				
				this.emit("messageReceived", {
					"author": req.body.author,
					"content": req.body.content
				});
			} else {
				res.status(400).send();
			}
		});

		app.use("/", _router);
		app.listen(this.port, this.address)
	}
}

module.exports = { API }