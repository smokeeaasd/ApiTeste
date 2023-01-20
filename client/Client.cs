using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Smokeesz
{
	public abstract class Client
	{
		protected abstract string Username { get; }

		private bool Started = false;
		private bool Stopped = false;

		protected virtual void OnMessage(Message msg) { }
		protected virtual void OnReady() { }
		protected virtual void OnError(Exception ex) { }

		protected void SendMessage(string msg) {
			var request = new RestRequest();
			var client = new RestClient("http://localhost:8000/send");

			request.Method = Method.Post;

			request.AddHeader("Content-Type", "application/json");
			request.AddHeader("Accept", "application/json, text-plain, */*");

			request.AddBody(new Message() {
				Author = Username,
				Content = msg
			});

			client.Execute(request);
		}

		public void Start()
		{
			if (!Started)
			{
				this.OnReady();
				this.Started = true;
			}

			if (Stopped)
				return;

			var request = new RestRequest();
			var client = new RestClient("http://localhost:8000/msg");
			request.Method = Method.Get;

			var res = client.ExecuteGet(request).Content;

			if (res == null)
			{
				Exception ex = new Exception("Invalid response");
				this.OnError(ex);
			}
			else
			{
				object messageObj = JsonConvert.DeserializeObject(res) ?? false;

				JObject message = (JObject)messageObj;

				this.OnMessage(new Message()
				{
					Author = message["author"]?.ToString() ?? "Desconhecido",
					Content = message["content"]?.ToString() ?? "Desconhecido"
				});

				this.Start();
			}
		}

		public void Stop()
		{
			Stopped = true;
		}
	}
}