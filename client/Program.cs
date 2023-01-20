using System;

namespace Smokeesz
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.Clear();
			
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write("Olá! ");

			Console.ResetColor();
			Console.Write("seja bem vindo(a) ao ");
			Console.ForegroundColor = ConsoleColor.Magenta;

			Console.Write("chat.");
			Console.ResetColor();
			Console.Write(" insira o seu nome: ");

			string nome = Console.ReadLine();
			
			if (string.IsNullOrWhiteSpace(nome))
			{
				nome = "Usuário sem nome.";
			}

			Console.Title = nome;

			App app = new App(nome);

			app.Start();
		}
	}
}