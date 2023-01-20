const { API } = require("./api");

const api = new API(8000, "localhost");

api.on("ready", () => {
	console.log("API iniciada.");
});

api.router.get("/msg", (req, res) => {
	api.once("messageReceived", (msg) => {
		res.send(msg);
	});
});

api.start();