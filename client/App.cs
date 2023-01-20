using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Smokeesz
{
	public sealed class App : Client
	{
		public App(string username)
		{
			this.Username = username;
		}

		protected override async void OnReady()
		{
			Console.Clear();
			await Task.Run(() =>
			{
				while (true)
				{
					Console.ForegroundColor = ConsoleColor.Cyan;
					Console.Write(">> ");
					Console.ResetColor();

					string? msg = Console.ReadLine();
					if (!string.IsNullOrWhiteSpace(msg))
					{
						if (msg == "/stop")
						{
							Stop();
							break;
						}
						else
						{
							SendMessage(msg);
						}
					}
				}
			});
		}

		protected override void OnMessage(Message msg)
		{
			Console.Clear();
			messages.Add(msg);
			Console.ForegroundColor = ConsoleColor.Green;

			foreach (Message m in messages)
			{
				Console.ForegroundColor = ConsoleColor.Green;
				Console.Write($"{m.Author} enviou: ");
				Console.ResetColor();
				Console.WriteLine($"{m.Content}");
			}
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.Write(">> ");
			Console.ResetColor();
		}

		protected override string Username { get; }
		private List<Message> messages = new List<Message>();
	}
}