using System;

namespace Brick_o_matic.Viewer
{
	public static class Program
	{
		[STAThread]
		static void Main(string[] args)
		{
			if (args.Length == 0) return;
			using (var game = new Game(args[0]))
				game.Run();
		}
	}
}
